using System.Linq;
using FreeCellSolitaire.Core.CardModels;

namespace FreeCellSolitaire.Tests
{
    public class DeckTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Deck發牌基本測試()
        {
            var deck = Deck.Create().Shuffle(101);
            Assert.AreEqual(deck.GetCardsCount(), 52);
            var card = deck.Pick();
            Assert.IsTrue(card != null);
            Assert.AreEqual(deck.GetCardsCount(), 51);
            deck.Reset();
            Assert.AreEqual(deck.GetCardsCount(), 52);

            var card2 = deck.Pick();
            Assert.AreEqual(card, card2);
        }

        [Test]
        public void PickTest拿完總數應該是52且不重複()
        {
            var deck = Deck.Create().Shuffle(101);
            List<Card> myCards = new List<Card>();
            Card card ;
            while (true)
            {
                card = deck.Pick();
                if (card == null)
                {
                    break;
                }
                myCards.Add(card);
            };
            Assert.AreEqual(myCards.Count, 52);
            Assert.AreEqual(myCards.Select(x => x).Distinct().Count(), 52);
        }

        [Test]
        public void ShuffleTest確定牌有洗亂()
        {
            var deck = Deck.Create().Shuffle(101);
            List<Card> myCards = new List<Card>();
            Card prevCard = null;
            Card card = null;
            int adjacentSumScore = 0;
            int adjacentCount = 0;
            while (true)
            {
                int baseScore = 3;
                prevCard = card;
                card = deck.Pick();
              
                //發完了
                if (card == null)
                {
                    break;
                }

                if (prevCard != null)
                {

                    if (prevCard.Suit == card.Suit)
                    {
                        baseScore--;
                    }
                    if (Math.Abs(prevCard.Number - card.Number) <= 1)
                    {
                        baseScore= baseScore - 2;
                    }
                    adjacentSumScore += baseScore;
                    adjacentCount++;
                }
            };
            Assert.Greater((double)adjacentSumScore / adjacentCount, 2);
        }
    }
}