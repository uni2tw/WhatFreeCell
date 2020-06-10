using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace CoreForm.UI
{
    /// <summary>
    /// 右上完成區
    /// </summary>
    public class CompletionZone : IZone
    {
        private IGameForm form;
        public event ZoneHolderHandler HolderClick;
        /// <summary>
        /// 初始 右上完成區
        /// </summary>
        /// <param name="form"></param>
        public CompletionZone(IGameForm form)
        {
            this.form = form;
            this.Slots = new List<Slot>();
        }

        public bool CanSwap
        {
            get
            {
                return false;
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
                return false;
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

        public void Init(int cardWidth, int cardHeight, int left, int top)
        {
            for (int i = 0; i < 4; i++)
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
                var slot = new Slot(left, top, cardHeight, holder, Capacity, i);
                Slots.Add(slot);

                holder.Click += delegate (object sender, EventArgs e)
                {
                    HolderClick?.Invoke(GameZoneType.Completion, slot);
                };

                left = left + cardWidth;
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

        public bool SetCard(int x, CardView card)
        {
            if (this.Slots[x].IsFull)
            {
                return false;
            }

            Slot slot = this.Slots[x];

            this.Slots[x].AddCard(card);
            card.View.Visible = true;
            card.View.Location = this.Slots[x].GetLocation(0);
            card.View.BringToFront();
            card.ZoneType = GameZoneType.Completion;
            return true;
        }

        public void RemoveCard(CardView selectedCard)
        {
            throw new NotImplementedException();
        }

        public bool TrySelect(int slotIndex, out string message)
        {
            throw new NotImplementedException();
        }

        public void DeselectSlots()
        {
            throw new NotImplementedException();
        }

        public int GetSlotSelectedIndex()
        {
            throw new NotImplementedException();
        }

        public CardLocation GetSelectedInfo()
        {
            return null;
        }
    }
}
