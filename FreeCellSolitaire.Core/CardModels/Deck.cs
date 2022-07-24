using FreeCellSolitaire.Entities.GameEntities;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace FreeCellSolitaire.Core.CardModels
{
    /// <summary>
    /// 一副牌
    /// </summary>
    public class Deck
    {
        public Deck(int cardCount)
        {
            _cards = new Card[cardCount];
        }
        private Card[] _cards;
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < _cards.Length; i++)
            {
                if (i > 0)
                {
                    sb.Append(", ");
                }
                sb.AppendLine(string.Format("{0}", _cards[i].ToString()));
            }
            return sb.ToString();
        }

        int pointer = 0;
        public Card Pick()
        {
            if (GetCardsCount() == 0)
            {
                return null;
            }
            var result = _cards[pointer];
            pointer++;
            return result;
        }

        public void Reset()
        {
            pointer = 0;
        }

        public int GetCardsCount()
        {
            return _cards.Length - pointer ;
        }

        /// <summary>
        /// 建立一副撲克牌的資料(有序)
        /// </summary>
        /// <returns></returns>
        public static Deck Create(int cardCount = 52)
        {
            Deck deckCards = new Deck(cardCount);
            int pos = 0;
            bool full = false;
            for (int i = 0; i < 13; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (pos >= cardCount)
                    {
                        full = true;
                        break;
                    }
                    var card = new Card
                    {
                        Number = i + 1,
                        Suit = (CardSuit)j,
                    };
                    deckCards._cards[pos] = card;
                    pos++;
                }
                if (full)
                {
                    break;
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
            List<Card> temp = new List<Card>(_cards);
            int pos = 0;
            while (temp.Count > 0)
            {
                int from = rand.Next(temp.Count);
                _cards[pos] = temp[from];
                pos++;
                temp.RemoveAt(from);
            }
            return this;
        }
    }

}
