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
                int total;
                List<TrackList> completions;
                bool doable = (game as Game).FindTheEnd(out total, out completions);
                Assert.IsTrue(doable);
                Console.WriteLine(string.Join(',', completions.First()));
            }
            //s2 是無法完成的
            {
                var game = game0.Clone();
                game.Tableau.GetColumn(0).AddCards("s2");
                int total;
                List<TrackList> completions;
                bool doable = (game as Game).FindTheEnd(out total, out completions);
                Assert.IsFalse(doable);                
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

                int total;
                List<TrackList> completions;
                bool doable = (game as Game).FindTheEnd(out total, out completions);
                Assert.IsTrue(doable);
                Console.WriteLine("Successful Steps:" + completions.First().ToString());
            }
            
        }

        [Test]
        public void ThreeCardsAtTableau()
        {
            throw new NotImplementedException();
            IGame game0 = new Game() { EnableAssist = true };
            var tableau = new Tableau(game0);
            var homecells = new Homecells(game0);
            var foundations = new Foundations(game0);

            {
                var game = game0.Clone();
                game.Tableau.GetColumn(0).AddCards("s1,s2");
                Console.WriteLine(game.GetDebugInfo($"s-0") + $" - completed is {game.IsCompleted()}");

                int total;
                List<TrackList> completions;
                bool doable = (game as Game).FindTheEnd(out total, out completions);
                Assert.IsTrue(doable);
                Console.WriteLine("Successful Steps:" + completions.First().ToString());
            }

        }
    }
}