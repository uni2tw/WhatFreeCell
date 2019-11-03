using System;
using System.Collections.Generic;

namespace CoreForm.UI
{
    public class WaitingZone
    {
        private List<WaitingSlot> slots = new List<WaitingSlot>();

        public WaitingZone()
        {
            slots.Add(new WaitingSlot());
            slots.Add(new WaitingSlot());
            slots.Add(new WaitingSlot());
            slots.Add(new WaitingSlot());
        }

        private bool AppendCard(Card card)
        {
            return true;
        }

        public int GetSlotCount()
        {
            return slots.Count;
        }

        public int GetSlotCardLimit()
        {
            return 13;
        }

        public void Init(int dataX, int dataY, int left, int top)
        {
            slots[dataX].Cards[dataY] = new WaitingCard
            {
                Left = left,
                Top = top,
                Card = null
            };
        }
    }
    
}
