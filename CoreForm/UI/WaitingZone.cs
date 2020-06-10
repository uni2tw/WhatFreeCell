using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CoreForm.UI
{    
    /// <summary>
    /// 下方待處理區
    /// </summary>
    public class WaitingZone : IZone
    {
        private IGameForm form;

        public WaitingZone(IGameForm form)
        {
            this.form = form;
            this.Slots = new List<Slot>();
        }

        public bool CanSwap
        {
            get
            {
                return true;
            }
        }
        public bool CanMoveIn
        {
            get
            {
                return true;
            }
        }
        public bool CanMoveOut
        {
            get
            {
                return true;
            }
        }
        public int Capacity
        {
            get
            {
                return 13;
            }
        }

        public List<Slot> Slots { get; set; }
        public event ZoneHolderHandler HolderClick;

        public void Init(int cardWidth, int cardHeight, int left, int top, int marginLeft, int marginTop)
        {
            for (int i = 0; i < 8; i++)
            {
                Control holder = new Panel
                {
                    Location = new Point(left, top),
                    Width = cardWidth,
                    Height = cardHeight,
                    BorderStyle = BorderStyle.FixedSingle,
                    BackgroundImageLayout = ImageLayout.Stretch
                };

                form.SetControlReady(holder);
                var slot = new Slot(left, top, cardHeight, holder, this.Capacity, i);
                Slots.Add(slot);
                holder.Click += delegate (object sender, EventArgs e)
                {
                    HolderClick?.Invoke(GameZoneType.Waiting, slot);
                };
                left = left + cardWidth + marginLeft;
            }
        }

        public bool IsAvailableFor(int x, CardView card)
        {
            if (this.Slots[x].IsFull)
            {
                return false;
            }
            CardView lastCard = this.Slots[x].LastCard();
            if (lastCard == null && card.Number == 1)
            {
                return true;
            }
            if (lastCard != null && lastCard.Suit == card.Suit && card.Number - lastCard.Number == 1)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x">slot's number</param>
        /// <param name="card"></param>
        /// <returns></returns>
        public bool SetCard(int x, CardView card)
        {
            if (this.Slots[x].IsFull)
            {
                return false;
            }

            Slot slot = this.Slots[x];
            
            card.View.Visible = true;
            card.View.Location = this.Slots[x].GetLocation(slot.Count);
            card.View.BringToFront();
            card.ZoneType = GameZoneType.Waiting;
            card.Slot = slot;
            slot.AddCard(card);
            card.View.Click += delegate (object sender, EventArgs e)
            {
                HolderClick?.Invoke(GameZoneType.Waiting, slot);
            };
            return true;
        }

        public CardView SelectLastCard(int x)
        {
            Slot slot = this.Slots[x];
            var lastCard = slot.LastCard();
            if (lastCard == null)
            {
                return null;
            }
            lastCard.Actived = true;
            return lastCard;
        }

        public List<CardView> RemoveCard(CardView theCard)
        {
            int start;
            List<CardView> cards = FindCards(theCard, out start);
            foreach(var card in cards)
            {
                card.ZoneType = GameZoneType.None;
                card.View.Visible = false;
                card.Slot.RemoveCard(card);
            }            

            return cards;
        }        

        public List<CardView> FindCards(CardView theCard, out int start)
        {
            List<CardView> result = new List<CardView>();
            start = -1;
            foreach(var slot in Slots)
            {                
                var cards = slot.GetCards();
                for (int i= 0;i< cards.Count;i++)
                {
                    var card = cards[i];
                    if (card.Equals(theCard))
                    {
                        result.Add(card);
                        start = i;
                        return result;
                    }
                }
            }
            return result;
        }

        public int GetSlotSelectedIndex()
        {
            foreach (var slot in this.Slots)
            {
                var lastCard = slot.LastCard();
                if (lastCard == null)
                {
                    continue;
                }
                if (lastCard.Actived)
                {
                    return slot.Index;
                }
            }
            return -1;
        }

        public CardLocation GetSelectedInfo()
        {
            foreach (var slot in this.Slots)
            {
                var lastCard = slot.LastCard();
                if (lastCard == null)
                {
                    continue;
                }
                if (lastCard.Actived)
                {
                    return new CardLocation
                    {
                        ZoneType = GameZoneType.Waiting,
                        SlotIndex = slot.Index,
                        DataView = lastCard
                    };
                }
            }
            return null;
        }

        public bool TrySelect(int slotIndex, out string message)
        {
            message = string.Empty;
            if (this.GetSlotSelectedIndex() == slotIndex)
            {
                return false;
            }
            if (this.GetSlotSelectedIndex() != -1)
            {
                message = "此步犯規";
                return false;
            }
            this.SelectLastCard(slotIndex);
            return true;
        }

        public void DeselectSlots()
        {
            foreach(var slot in this.Slots)
            {
                var lastCard = slot.LastCard();
                if (lastCard == null)
                {
                    continue;
                }
                lastCard.Actived = false;               
            }                       
        }
    }
}
