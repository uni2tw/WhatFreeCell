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
            cardView.SetCard(card, false);
            return true;
        }

        private CardView GetEmptyCardView()
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

        public CardView SelectCard(int no)
        {
            return slots[no].SelectLastCard();
        }

        public CardView GetActivedCard()
        {
            foreach (var slot in slots)
            {
                foreach(var cardView in slot.CardViews)
                {
                    if (cardView.Actived)
                    {
                        return cardView;
                    }
                }
            }
            return null;
        }

        private CardView GetCardView(int x, int y)
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

        public void InitView(int dataX, int dataY, Control viewControl)
        {
            var slot = slots[dataX];
            slot.CardViews[dataY] = new CardView
            {
                Slot = slot,
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
