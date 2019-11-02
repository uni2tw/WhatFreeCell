using System;
using System.Collections.Generic;
using System.Linq;

namespace CoreForm
{
    public class FreeCell
    {
        public FreeCell()
        {
            Start = new StartArea();
            Completion = new CompletionArea();
            Temp = new TempArea();
        }
        public StartArea Start { get; set; }
        public CompletionArea Completion { get; set; }
        public TempArea Temp { get; set; }        

        public void NewGame(int seed)
        {
            var deck = Deck.Create().Shuffle(seed);

            //put deck to start area
            while (true)
            {
                Card card = deck.Draw();                
                if (card == null) { 
                    break; 
                }
                Start.Put(card);
            }
            //Console.WriteLine(deck.ToString());
        }

        public class AreaSlot
        {            
            public string Name { get; set; }
            public AreaSlot()
            {
                CardLines = new List<Card>();
            }
            public List<Card> CardLines { get; set; }
            public int Limit { get; set; }
            public override string ToString()
            {
                return string.Format("Slot {0} with {1} cards", Name, CardLines.Count);
            }
        }

        public abstract class Area
        {

            public AreaSlot[] Slots { get; set; }
            public abstract int Limit { get;  }


        }
        public class TempArea : Area
        {
            public override int Limit
            {
                get
                {
                    return 1;
                }
            }
        }

        public class CompletionArea : Area
        {
            public override int Limit
            {
                get
                {
                    return 13;
                }
            }
        }

        public class StartArea : Area
        {
            public StartArea()
            {
                Slots = new AreaSlot[] {
                    new AreaSlot{ Name = "A", Limit = this.Limit },
                    new AreaSlot{ Name = "B", Limit = this.Limit }, 
                    new AreaSlot{ Name = "C", Limit = this.Limit }, 
                    new AreaSlot{ Name = "D", Limit = this.Limit }
                };
            }
            public override int Limit {
                get
                {
                    return 52;
                }
            }
                    
            public bool Put(Card card)
            {
                var slot = PickSlot();
                if (slot == null)
                {
                    return false;
                }
                slot.CardLines.Add(card);
                return true;
            }

            public AreaSlot PickSlot()
            {                
                return Slots.Where(t => t.CardLines.Count < t.Limit).OrderBy(t => t.CardLines.Count)
                    .FirstOrDefault();
            }
        }
    }
    
}
