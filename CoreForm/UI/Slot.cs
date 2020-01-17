using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace CoreForm.UI
{
    /// <summary>
    /// 一排
    /// </summary>
    public class Slot
    {
        public int right { get; private set; }
        public int top { get; private set; }
        public int cardHeight { get; private set; }

        public int Index { get; set; }
        /// <summary>
        /// 一列最多可以放幾張牌
        /// </summary>
        public int Capacity { get; set; }

        public int Count { 
            get
            {
                return Cards.Count;
            } 
        }

        public Slot(int right, int top, int cardHeight, 
            Control holder, int capacity, int index)
        {
            this.right = right;
            this.top = top;
            this.cardHeight = cardHeight;
            this.Holder = holder;
            this.Capacity = capacity;
            this.Index = index;
            Cards = new List<CardView>();
        }

        public Point GetLocation(int y)
        {
            return new Point(right, top + y * (cardHeight / 6));
        }

        private List<CardView> Cards { get; set; }
        /// <summary>
        /// 支架
        /// </summary>
        public Control Holder { get; set; }
        public bool IsFull
        {
            get
            {
                return this.Cards.Count >= this.Capacity;
            }
        }

        public bool AddCard(CardView card)
        {
            if (Cards == null)
            {
                Cards = new List<CardView>();
            }
            if (this.IsFull == false)
            {
                this.Cards.Add(card);
                return true;
            } 
            else
            {
                return false;
            }
        }

        public CardView LastCard()
        {
            return Cards.LastOrDefault();
        }

        public CardView GetCard(int i)
        {
            return Cards[i];
        }

        public void FindCard(CardView selectedCard)
        {
            throw new NotImplementedException();
        }

        public List<CardView> GetCards()
        {
            return this.Cards;
        }

        public void RemoveCard(CardView theCard)
        {
            this.Cards.Remove(theCard);
        }
    }



}
