using FreeCellSolitaire.Core.GameModels;
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
        public const int MaxNumber = 10000;
        public int Number { get; set; }
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
        public Deck Shuffle(int? number)
        {
            if (number == null)
            {
                number = GetRandom();
            }
            this.Number = number.Value;
            if (DebugDeck(number.Value))
            {
                return this;
            }
            Random rand = new Random(number.Value);
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

        private bool DebugDeck(int seed)
        {
            if (seed == 26458)
            {
                List<Card> temp = new List<Card>();
                temp.Add(new Card { Suit = CardSuit.Spade, Number = 13 });
                temp.Add(new Card { Suit = CardSuit.Heart, Number = 12 });
                temp.Add(new Card { Suit = CardSuit.Club, Number = 2 });
                temp.Add(new Card { Suit = CardSuit.Diamond, Number = 8 });
                temp.Add(new Card { Suit = CardSuit.Diamond, Number = 11 });
                temp.Add(new Card { Suit = CardSuit.Club, Number = 4 });
                temp.Add(new Card { Suit = CardSuit.Heart, Number = 5 });
                temp.Add(new Card { Suit = CardSuit.Club, Number = 1 });

                temp.Add(new Card { Suit = CardSuit.Heart, Number = 11 });
                temp.Add(new Card { Suit = CardSuit.Diamond, Number = 12 });
                temp.Add(new Card { Suit = CardSuit.Club, Number = 5 });
                temp.Add(new Card { Suit = CardSuit.Heart, Number = 6 });
                temp.Add(new Card { Suit = CardSuit.Spade, Number = 9 });
                temp.Add(new Card { Suit = CardSuit.Club, Number = 12 });
                temp.Add(new Card { Suit = CardSuit.Spade, Number = 10 });
                temp.Add(new Card { Suit = CardSuit.Diamond, Number = 10 });

                temp.Add(new Card { Suit = CardSuit.Spade, Number = 5 });
                temp.Add(new Card { Suit = CardSuit.Club, Number = 13 });
                temp.Add(new Card { Suit = CardSuit.Club, Number = 6 });
                temp.Add(new Card { Suit = CardSuit.Club, Number = 9 });
                temp.Add(new Card { Suit = CardSuit.Diamond, Number = 7 });
                temp.Add(new Card { Suit = CardSuit.Diamond, Number = 3 });
                temp.Add(new Card { Suit = CardSuit.Diamond, Number = 2 });
                temp.Add(new Card { Suit = CardSuit.Spade, Number = 3 });

                temp.Add(new Card { Suit = CardSuit.Club, Number = 11 });
                temp.Add(new Card { Suit = CardSuit.Diamond, Number = 13 });
                temp.Add(new Card { Suit = CardSuit.Spade, Number = 7 });
                temp.Add(new Card { Suit = CardSuit.Spade, Number = 11 });
                temp.Add(new Card { Suit = CardSuit.Heart, Number = 3 });
                temp.Add(new Card { Suit = CardSuit.Heart, Number = 13 });
                temp.Add(new Card { Suit = CardSuit.Heart, Number = 10 });
                temp.Add(new Card { Suit = CardSuit.Spade, Number = 2 });

                temp.Add(new Card { Suit = CardSuit.Spade, Number = 8 });
                temp.Add(new Card { Suit = CardSuit.Diamond, Number = 5 });
                temp.Add(new Card { Suit = CardSuit.Diamond, Number = 4 });
                temp.Add(new Card { Suit = CardSuit.Diamond, Number = 9 });
                temp.Add(new Card { Suit = CardSuit.Spade, Number = 12 });
                temp.Add(new Card { Suit = CardSuit.Club, Number = 7 });
                temp.Add(new Card { Suit = CardSuit.Spade, Number = 1 });
                temp.Add(new Card { Suit = CardSuit.Spade, Number = 4 });

                temp.Add(new Card { Suit = CardSuit.Heart, Number = 9 });
                temp.Add(new Card { Suit = CardSuit.Heart, Number = 2 });
                temp.Add(new Card { Suit = CardSuit.Diamond, Number = 1 });
                temp.Add(new Card { Suit = CardSuit.Spade, Number = 6 });
                temp.Add(new Card { Suit = CardSuit.Club, Number = 10 });
                temp.Add(new Card { Suit = CardSuit.Diamond, Number = 6 });
                temp.Add(new Card { Suit = CardSuit.Heart, Number = 8 });
                temp.Add(new Card { Suit = CardSuit.Heart, Number = 7 });

                temp.Add(new Card { Suit = CardSuit.Heart, Number = 4 });
                temp.Add(new Card { Suit = CardSuit.Heart, Number = 1 });
                temp.Add(new Card { Suit = CardSuit.Club, Number = 3 });
                temp.Add(new Card { Suit = CardSuit.Club, Number = 8 });


                _cards = temp.ToArray();
                return true;
            }
            return false;
        }

        public int GetRandom()
        {
            Random seedRand = new Random();
            return seedRand.Next(10000) + 1;
        }
    }

}
