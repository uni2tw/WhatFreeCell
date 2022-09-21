using FreeCellSolitaire.Core.GameModels;
using FreeCellSolitaire.Entities.GameEntities;
using System.Security.Cryptography.X509Certificates;

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
        public void ManyCardsAtTableau()
        {            
            IGame game0 = new Game() { EnableAssist = true };
            var tableau = new Tableau(game0);
            var homecells = new Homecells(game0);
            var foundations = new Foundations(game0);

            {
                DateTime now = DateTime.Now;
                var game = game0.Clone();
                game.Tableau.GetColumn(0).AddCards("s1,s2,s3,s4,s5");
                Console.WriteLine(game.GetDebugInfo($"s-0") + $" - completed is {game.IsCompleted()}");

                int total;
                List<TrackList> completions;
                bool doable = (game as Game).FindTheEnd(out total, out completions);
                Assert.IsTrue(doable);
                Console.WriteLine($"Total samples: {total}");
                Console.WriteLine("Successful steps:" + completions.First().ToString());
                Assert.Less((DateTime.Now - now).TotalSeconds, 0.5,"執行速度太慢");

            }

            
        }
        [Test]
        public void GameCloneSpeedTest()
        {
            IGame game0 = new Game() { EnableAssist = true };
            var tableau = new Tableau(game0);
            var homecells = new Homecells(game0);
            var foundations = new Foundations(game0);

            {
                DateTime now = DateTime.Now;
                var game = game0.Clone();
                game.Tableau.GetColumn(0).AddCards("s1,s2,s3,s4");
                Console.WriteLine(game.GetDebugInfo($"s-0") + $" - completed is {game.IsCompleted()}");
              

                for (int i = 0; i < 2000; i++)
                {
                    game.Clone();
                }
                var elapsedSecs = (DateTime.Now - now).TotalSeconds;
                Console.WriteLine($"elapsed secs: {elapsedSecs.ToString("0.00")}");
                Assert.Less(elapsedSecs, 0.5, "執行速度太慢");
            }

        }
    }
}