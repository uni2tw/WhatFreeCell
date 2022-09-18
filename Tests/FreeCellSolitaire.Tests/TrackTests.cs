using FreeCellSolitaire.Core.GameModels;
using FreeCellSolitaire.Entities.GameEntities;

namespace FreeCellSolitaire.Tests
{
    public class TrackTests
    {
        IGame game0;
        [SetUp]
        public void Setup()
        {
            game0 = new Game() { EnableAssist = true };
            var tableau = new Tableau(game0);
            var homecells = new Homecells(game0);
            var foundations = new Foundations(game0);
        }

        [Test]
        public void OneCardAtTableau()
        {
            IGame game0 = new Game() { EnableAssist = true };
            var tableau = new Tableau(game0);
            var homecells = new Homecells(game0);
            var foundations = new Foundations(game0);

            var game = game0.Clone();
            game.Tableau.GetColumn(0).AddCards("s1,s2,s3");
            game.DebugInfo("step 0");
            Assert.AreEqual(0, game.GetTracks().Count);
            game.Move("t0f0");
            game.DebugInfo("step 1");
            Assert.AreEqual(1, game.GetTracks().Count);
            game.Move("t0f1");
            game.DebugInfo("step 2");
            Assert.AreEqual(2, game.GetTracks().Count);

            // clone test
            var game2 = game.Clone();
            game.DebugInfo("clone");
            Assert.AreEqual(2, game2.GetTracks().Count);
        }
    }
}