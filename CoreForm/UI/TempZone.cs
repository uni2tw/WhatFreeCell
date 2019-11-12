using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace CoreForm.UI
{
    public enum GameZoneType
    {
        Temp, Completion, Waiting
    }
    public class TempZone
    {
        const int Limit = 1;
        List<Slot> slots = new List<Slot>();
        public TempZone()
        {
            slots.Add(new Slot());
            slots.Add(new Slot());
            slots.Add(new Slot());
            slots.Add(new Slot());
        }

        public bool InitView(Control viewControl, int n = 0)
        {
            Slot slot = null;
            if (slots[n].CardViews.Count < Limit)
            {
                slot = slots[n];
            }
            if (slot == null)
            {
                for (int i = 0; i < slots.Count; i++)
                {
                    if (slots[i].CardViews.Count < Limit)
                    {
                        slot = slots[i];
                        break;
                    }
                }
            }
            if (slot == null)
            {
                return false;
            }
            var cardView = new CardView
            {
                View = viewControl,
                Data = null,
                Slot = slot
            };
            slot.CardViews.Add(cardView);
            return true;
        }

        public void Reset()
        {
            foreach (var slot in slots)
            {
                foreach (var cardView in slot.CardViews)
                {
                    cardView.Data = null;
                }
            }
        }

        public CardView SelectCardView(int slotNo)
        {
            foreach (var cardView in slots[slotNo].CardViews)
            {
                if (cardView != null)
                {
                    return cardView;
                }
            }            
            return null;
        }

        public CardView GetActtivedCard()
        {
            foreach (var slot in slots)
            {
                foreach (var cardView in slot.CardViews)
                {
                    if (cardView.Actived)
                    {
                        return cardView;
                    }
                }
            }
            return null;
        }
    }

    public class Slot : ISlot
    {
        public string Name { get; set; }
        public List<CardView> CardViews { get; private set; }        

        public ZoneType Type { get; set; }

        public Slot()
        {
            CardViews = new List<CardView>();
        }

        public void CardClicked()
        {
            SetLastCardActived();
        }

        public void CardDoubleClicked()
        {
            throw new NotImplementedException();
        }

        public bool SetLastCardActived()
        {
            var card = SelectLastCard();
            if (card == null)
            {
                return false;
            }
            card.Actived = !card.Actived;
            return true;
        }
        public CardView SelectLastCard()
        {
            var card = this.CardViews.LastOrDefault(t => t.Data != null);
            return card;
        }

    }

    public class CompletionZone
    {
        const int Limit = 13;
        List<Slot> slots = new List<Slot>();
        public CompletionZone()
        {
            slots.Add(new Slot());
            slots.Add(new Slot());
            slots.Add(new Slot());
            slots.Add(new Slot());
        }

        public void Reset()
        {
            foreach (var slot in slots)
            {
                foreach (var cardView in slot.CardViews)
                {
                    cardView.Data = null;
                }
            }
        }

        public bool InitView(CardView cardView, int slotNo)
        {
            Slot slot = slots[slotNo];
            if (slot.CardViews.Count >= Limit)
            {
                return false;
            }
            slot.CardViews.Add(cardView);
            return true;
        }
    }
}
