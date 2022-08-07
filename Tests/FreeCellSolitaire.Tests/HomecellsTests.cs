using FreeCellSolitaire.Core.GameModels;
using FreeCellSolitaire.Entities.GameEntities;

namespace FreeCellSolitaire.Tests
{
    public class HomecellsTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void MoveFail()
        {
            IGame game = new Game();
            var tableau = new Tableau(game);
            var homecells = new Homecells(game);
            var foundations = new Foundations(game);
            
            tableau.GetColumn(0).AddCards("s13");

            var srcCard = tableau.GetColumn(0).GetLastCard();

            Assert.IsFalse(srcCard.Move(homecells.GetColumn(0)));
        }
    }
}