using System;
using System.Collections.Generic;
using System.Windows.Forms;

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

        public void Init(int dataX, int dataY, Control viewControl)
        {
            slots[dataX].Cards[dataY] = new CardView
            {
                View = viewControl,                 
                Data = null
            };
        }

        public void Clear()
        {
            foreach(var slot in slots)
            {
                foreach(var x in slot.Cards)
                {
                    (x.View as System.Windows.Forms.PictureBox).Image = null;
                    x.Data = null;
                }
            }            
        }
    }
    
}
