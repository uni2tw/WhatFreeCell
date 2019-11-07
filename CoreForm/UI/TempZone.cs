using System;
using System.Collections.Generic;
using System.Linq;

namespace CoreForm.UI
{
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

        public bool InitView(CardView cardView, int n = 0)
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
            foreach (var slot in slots)
            {
                foreach (var cardView in slot.CardViews)
                {
                    return cardView;
                }
            }
            return null;
        }
    }

    public class Slot
    {
        public string Name { get; set; }
        public List<CardView> CardViews { get; private set; }

        public Slot()
        {
            CardViews = new List<CardView>();
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

        public bool InitView(CardView cardView, int n=0)
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
            slot.CardViews.Add(cardView);
            return true;
        }
    }
}
