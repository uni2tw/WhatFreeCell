using CoreForm.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace FreeCell.Entities.GameEntities
{
    /// <summary>
    /// 右上完成區
    /// </summary>
    public class FreeCells : IZone
    {
        private IGameForm form;
        public event ZoneHolderHandler HolderClick;
        /// <summary>
        /// 初始 右上完成區
        /// </summary>
        /// <param name="form"></param>
        public FreeCells(IGameForm form)
        {
            this.form = form;
            Slots = new List<Slot>();
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
                var slot = new Slot(left, top, cardHeight, holder, Capacity, i, GameZoneType.Completion, this);
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
            if (Slots[x].IsFull)
            {
                return false;
            }
            CardView lastCard = Slots[x].LastCard();
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
        public bool MoveCard(int slotIndex, CardView card)
        {
            Slot pSlot = card.Slot;
            if (CheckCanMoveIn(pSlot, card) == false)
            {
                return false;
            }
            pSlot.RemoveCard(card);
            card.Actived = false;
            return SetCard(slotIndex, card);
        }

        private bool CheckCanMoveIn(Slot pSlot, CardView newCard)
        {
            var lastCard = pSlot.LastCard();
            if (lastCard == null && newCard.Number == 1)
            {
                return true;
            }
            else if (lastCard.Suit == newCard.Suit && newCard.Number - lastCard.Number == 1)
            {
                return true;
            }
            return false;
        }

        public bool SetCard(int x, CardView card)
        {
            if (Slots[x].IsFull)
            {
                return false;
            }

            Slot slot = Slots[x];

            Slots[x].AddCard(card);
            card.View.Visible = true;
            card.View.Location = Slots[x].GetLocation(0);
            card.View.BringToFront();
            card.ZoneType = GameZoneType.Completion;
            card.Slot = slot;
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

        CardMoveAction IZone.TryAction(int slotIndex, out string message)
        {
            throw new NotImplementedException();
        }

    }
}
