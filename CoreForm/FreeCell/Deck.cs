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

        /// <summary>
        /// 照目前指位器的位置取出一張牌，並將指位器移到下一張
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// 建立一副撲克牌的資料(有序)
        /// </summary>
        /// <returns></returns>
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
        /// <summary>
        /// 依seed亂數洗牌
        /// </summary>
        /// <param name="seed"></param>
        /// <returns></returns>
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
            return this;
        }
    }
    
}
