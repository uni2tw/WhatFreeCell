using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using CoreForm.Utilities;

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
            slots.Add(new WaitingSlot());
            slots.Add(new WaitingSlot());
            slots.Add(new WaitingSlot());
            slots.Add(new WaitingSlot());
        }

        public bool AppendCard(Card card)
        {
            if (card == null)
            {
                return false;
            }
            var cardView = GetEmptyCardView();
            (cardView.View as PictureBox).Image = card.Image;
            cardView.Data = card;
            return true;
        }

        public CardView GetEmptyCardView()
        {
            int y = 0;
            for (y = 0; y < 13; y++)
            {
                foreach (var slot in slots)
                {
                    if (slot.CardViews[y].Data == null)
                    {
                        return slot.CardViews[y];
                    }
                }
            }
            return null;
        }
        public CardView GetCardView(int x, int y)
        {
            return slots[x].CardViews[y];
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
            slots[dataX].CardViews[dataY] = new CardView
            {
                View = viewControl,                 
                Data = null
            };
        }

        public void Refresh()
        {
            for (int i = 0; i < GetSlotCount(); i++)
            {
                for (int j = 0; j < GetSlotCardLimit(); j++)
                {
                    CardView cardView = GetCardView(i, j);
                    if (cardView.Data != null)
                    {
                        cardView.View.BringToFront();
                    }
                }
            }
        }

        public void Clear()
        {
            foreach(var slot in slots)
            {
                foreach(var x in slot.CardViews)
                {
                    (x.View as System.Windows.Forms.PictureBox).Image = null;
                    x.Data = null;
                }
            }            
        }


    }
    
}
