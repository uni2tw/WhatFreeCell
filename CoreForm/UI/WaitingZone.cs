using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Media;
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

        public CardMoveAction TryAction(int slotIndex, out string message)
        {
            message = string.Empty;
            var srcSlotIndex = this.GetSlotSelectedIndex();
            if (srcSlotIndex == slotIndex)
            {
                this.DeselectSlots();
                return CardMoveAction.Deselect;
            }
            if (srcSlotIndex != -1)
            {
                //this.Slots[slotIndex].LastCard()
                //step 1 判斷可移動的卡片數
                //取得新位置的最後一張卡號
                //目前位置的卡後開始從最大張數判斷可否搬移

                //TODO 判斷犯規或是可移動 

                var srcCard = this.Slots[srcSlotIndex].LastCard();
                var destCard = this.Slots[slotIndex].GetCards();
                int spareSpaces = 1;

                List<CardView> moveableCards;
                if (TryMove(this.Slots[srcSlotIndex], this.Slots[slotIndex], spareSpaces, out moveableCards))
                {
                    MessageBox.Show("移動 " + moveableCards.Count + " 牌");
                    this.DeselectSlots();
                    return CardMoveAction.Move;
                }
                else
                {
                    SystemSounds.Asterisk.Play();
                    MessageBox.Show("此步犯規");
                    this.DeselectSlots();
                    return CardMoveAction.Fail;
                }

            }
            this.SelectLastCard(slotIndex);
            return  CardMoveAction.Select;
        }

        private bool TryMove(Slot srcSlot, Slot destSlot, int spareSpaces, out List<CardView> moveableCards)
        {
            moveableCards = new List<CardView>();
            List<CardView> srcLinkedCards = new List<CardView>();            
            for (int i= 0 ; i< srcSlot.GetCards().Count; i++)
            {
                if (srcSlot.GetCards().Count - i > spareSpaces)
                {
                    continue;
                }
                var srcCard = srcSlot.GetCard(i);
                var srcLinkedLastCard = srcLinkedCards.LastOrDefault();
                if (srcLinkedLastCard == null)
                {
                    srcLinkedCards.Add(srcCard);
                } 
                else
                {
                    if (srcLinkedLastCard.CheckLinkable(srcCard) == false)
                    {
                        srcLinkedCards.Clear();                        
                    }
                    srcLinkedCards.Add(srcCard);                
                }                
            }
            var destCard = destSlot.LastCard();
            if (destCard == null)
            {
                moveableCards.AddRange(srcLinkedCards);
                return true;
            }
            
            for(int i=0;i< srcLinkedCards.Count; i++)
            {
                if (srcLinkedCards[i].CheckLinkable(destCard)) {
                    moveableCards.AddRange(srcLinkedCards.Skip(i));
                    return true;
                }
            }

            return false;
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

