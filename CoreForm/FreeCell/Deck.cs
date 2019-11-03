using System;
using System.Collections.Generic;
using System.Text;

namespace CoreForm
{
    /// <summary>
    /// 一副牌
    /// </summary>
    public class Deck
    {
        public Deck()
        {
            Cards = new Card[52];
        }
        public Card[] Cards { get; set; }        
        public int Pos { get; set; }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < Cards.Length; i++) {
                if (i > 0 )
                {
                    sb.Append(", ");
                }
                sb.AppendLine(string.Format("{0}", Cards[i].ToString()));
            }
            return sb.ToString();
        }

        public Card Draw()
        {
            Card result;
            if (Pos == Cards.Length)
            {
                return null;
            }
            result = Cards[Pos];
            Pos++;
            return result;
        }

        public Card Pick(int n)
        {
            Card result;
            result = Cards[n];            
            return result;
        }

        public static Deck Create()
        {
            Deck deckCards = new Deck();
            int pos = 0;
            for (int i = 0; i < 13; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    var card = new Card
                    {
                        Number = i + 1,
                        Suit = (CardSuit)j,
                    };
                    card.ReloadImage();
                    deckCards.Cards[pos] = card;
                    pos++;
                }
            }
            return deckCards;
        }
        public Deck Shuffle(int seed)
        {
            Random rand = new Random(seed);
            List<Card> temp = new List<Card>(Cards);
            int pos = 0;
            while (temp.Count > 0)
            {
                int from = rand.Next(temp.Count);
                Cards[pos] = temp[from];
                pos++;
                temp.RemoveAt(from);
            }
            
            //for (int i = 0; i < 26; i++)
            //{
            //    int from = rand.Next(Cards.Length);
            //    int to = rand.Next(Cards.Length);
            //    var temp = Cards[from];
            //    Cards[from] = Cards[to];
            //    Cards[to] = temp;
            //}
            return this;
        }
    }
    
}
