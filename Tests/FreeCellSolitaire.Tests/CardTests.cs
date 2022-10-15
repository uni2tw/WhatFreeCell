using FreeCellSolitaire.Core.CardModels;

namespace FreeCellSolitaire.Tests
{
    public class CardTests
    {
        [SetUp]
        public void Setup()
        {
        }
        [Test]
        public void EqualTest()
        {
            HashSet<Card> cards = new HashSet<Card>();
            foreach (CardSuit suit in Enum.GetValues(typeof(CardSuit)))
            {
                for (int number = 1; number <= 13; number++)
                {
                    cards.Add(new Card(suit, number));
                }
            }
            Assert.AreEqual(52, cards.Count);

            //相同撲克牌，預期加不進去，維持張數相同
            foreach (CardSuit suit in Enum.GetValues(typeof(CardSuit)))
            {
                for (int number = 1; number <= 13; number++)
                {
                    cards.Add(new Card(suit, number));
                }
            }
            Assert.AreEqual(52, cards.Count);
        }
    }
}