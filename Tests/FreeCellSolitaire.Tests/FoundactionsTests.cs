using FreeCellSolitaire.Core.GameModels;
using FreeCellSolitaire.Entities.GameEntities;

namespace FreeCellSolitaire.Tests
{
    public class FoundactionsTests
    {
        [SetUp]
        public void Setup()
        {
        }


        [Test]
        public void Clone()
        {
            var foundations = new Foundations(null);
            foundations.GetColumn(0).AddCards("h2");
            Assert.AreEqual("h2", foundations.GetColumn(0).ToNotation());
            var clone = foundations.Clone() as Foundations;
            Assert.AreEqual("h2", clone.GetColumn(0).ToNotation());
        }
    }
}