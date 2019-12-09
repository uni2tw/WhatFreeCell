using CoreForm.FreeCell;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CoreForm.UI
{
    public delegate void ZoneHolderHandler(GameZoneType zoneType, Slot slot);
    public enum GameZoneType
    {
        None, Temp, Completion, Waiting
    }
    public interface IZone2
    {
        bool CanSwap { get; }
        bool CanMoveIn { get; }
        bool CanMoveOut { get; }
        int Capacity { get; }

        List<Slot> Slots { get; set; }

        bool IsAvailableFor(int x, CardView card);

        void SetCard(int x, CardView card);
    }

    public class WaitingZone2 : IZone2
    {
        private IGameForm form;

        public WaitingZone2(IGameForm form)
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

        public void SetCard(int x, CardView card)
        {
            if (this.Slots[x].IsFull)
            {
                throw new Exception("出錯了");
            }

            Slot slot = this.Slots[x];

            card.View.Visible = true;
            card.View.Location = this.Slots[x].GetLocation(slot.Count);
            card.View.BringToFront();
            card.ZoneType = GameZoneType.Waiting;
            slot.AddCard(card);
        }

        public ICardBase SelectCard(int x)
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

        public List<ICardBase> RemoveCard(ICardBase selectedCard)
        {

            return null;
        }
    }

    public class TempZone2 : IZone2
    {
        IGameForm form;
        public event ZoneHolderHandler HolderClick;
        public TempZone2(IGameForm form)
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

        internal void RemoveCard(ICardBase selectedCard)
        {
            throw new NotImplementedException();
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
                return 1;
            }
        }

        public List<Slot> Slots { get; set; }
        public void Init(int cardWidth, int cardHeight, int right, int top)
        {
            for (int i = 0; i < 4; i++)
            {
                Control holder = new Panel
                {
                    Location = new Point(right, top),
                    Width = cardWidth,
                    Height = cardHeight,
                    BorderStyle = BorderStyle.FixedSingle,
                    BackgroundImageLayout = ImageLayout.Stretch
                };

                form.SetControlReady(holder);
                var slot = new Slot(right, top, cardHeight, holder, Capacity, i);
                Slots.Add(slot);
                holder.Click += delegate (object sender, EventArgs e)
                {
                    HolderClick?.Invoke(GameZoneType.Temp, slot);
                };
                right = right + cardWidth;
            }
        }

        public void SetCard(int x, CardView card)
        {
            //if (this.Slots[x].Cards.Count >= this.QueueLimit)
            if (this.Slots[x].IsFull)
            {
                throw new Exception("出錯了");
            }

            this.Slots[x].AddCard(card);
            card.View.Visible = true;
            card.View.Location = this.Slots[x].GetLocation(0);
            card.View.BringToFront();
            card.ZoneType = GameZoneType.Temp;

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
    }

    public class CompletionZone2 : IZone2
    {
        private IGameForm form;
        public event ZoneHolderHandler HolderClick;

        public CompletionZone2(IGameForm form)
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

        public void SetCard(int x, CardView card)
        {
            if (this.Slots[x].IsFull)
            {
                throw new Exception("出錯了");
            }

            Slot slot = this.Slots[x];

            this.Slots[x].AddCard(card);
            card.View.Visible = true;
            card.View.Location = this.Slots[x].GetLocation(0);
            card.View.BringToFront();
            card.ZoneType = GameZoneType.Completion;
        }

        internal void RemoveCard(ICardBase selectedCard)
        {
            throw new NotImplementedException();
        }
    }
}
