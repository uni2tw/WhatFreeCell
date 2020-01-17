using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CoreForm.UI
{
    public delegate void ZoneHolderHandler(GameZoneType zoneType, Slot slot);

    public interface IZone
    {
        bool CanSwap { get; }
        bool CanMoveIn { get; }
        bool CanMoveOut { get; }
        int Capacity { get; }

        List<Slot> Slots { get; set; }

        bool IsAvailableFor(int x, CardView card);

        bool SetCard(int x, CardView card);
    }

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
            return true;
        }

        public CardView SelectCard(int x)
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
    }

    public class TempZone : IZone
    {
        IGameForm form;
        public event ZoneHolderHandler HolderClick;
        public TempZone(IGameForm form)
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

        internal void RemoveCard(CardView selectedCard)
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

        public bool SetCard(int x, CardView card)
        {
            //if (this.Slots[x].Cards.Count >= this.QueueLimit)
            if (this.Slots[x].IsFull)
            {
                return false;
            }

            this.Slots[x].AddCard(card);
            card.View.Visible = true;
            card.View.Location = this.Slots[x].GetLocation(0);
            card.View.BringToFront();
            card.ZoneType = GameZoneType.Temp;
            return true;
        }

        public bool IsAvailableFor(int x, CardView card)
        {
            if (this.Slots[x].IsFull)
            {
                return false;
            }
            CardView lastCard = this.Slots[x].LastCard();
            if (lastCard == null)
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

    public class CompletionZone : IZone
    {
        private IGameForm form;
        public event ZoneHolderHandler HolderClick;

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
    }
}
