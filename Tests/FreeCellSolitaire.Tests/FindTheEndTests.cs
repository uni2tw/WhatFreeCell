using FreeCellSolitaire.Core.GameModels;
using FreeCellSolitaire.Entities.GameEntities;

namespace FreeCellSolitaire.Tests
{
    public class FindTheEndTests
    {
        [SetUp]
        public void Setup()
        {
        }
        [Test]
        public void OneCardAtTableau()
        {
            IGame game0 = new Game() { EnableAssist = true };
            var tableau = new Tableau(game0);
            var homecells = new Homecells(game0);
            var foundations = new Foundations(game0);

            {
                var game = game0.Clone();
                game.Tableau.GetColumn(0).AddCards("s1");
                int possibilities = (game as Game).FindTheEnd();
                Assert.AreEqual(16, possibilities);
            }
            {
                var game = game0.Clone();
                game.Tableau.GetColumn(0).AddCards("s13");
                int possibilities = (game as Game).FindTheEnd();
                Assert.AreEqual(12, possibilities);
            }
        }
        [Test]
        public void TwoCardsAtTableau()
        {
            IGame game0 = new Game() { EnableAssist = true };
            var tableau = new Tableau(game0);
            var homecells = new Homecells(game0);
            var foundations = new Foundations(game0);

            {
                var game = game0.Clone();                
                game.Tableau.GetColumn(0).AddCards("s1,s2");
                Console.WriteLine(game.GetDebugInfo($"s-0") + $" - completed is {game.IsCompleted()}");

                int possibilities = (game as Game).FindTheEnd();
                Assert.AreEqual(11+15, possibilities);
            }
            
        }
    }
}