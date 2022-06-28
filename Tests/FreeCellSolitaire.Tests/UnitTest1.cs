using FreeCellSolitaire.Core.CardModels;
using FreeCellSolitaire.Core.GameModels;

namespace FreeCellSolitaire.Tests
{
    public class TableauTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            var deck = Deck.Create().Shuffle(101);
            var tableau = new Tableau(null);
            tableau.Init(deck);
            tableau.DebugInfo();
        }
    }
}