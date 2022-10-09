using FreeCellSolitaire.Core.CardModels;
using FreeCellSolitaire.Core.GameModels;
using FreeCellSolitaire.Entities.GameEntities;

namespace FreeCellSolitaire.Tests
{
    public class CardViewTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void s01_ColorTest()
        {
            CardView card1 = new CardView(null, new Card { Suit = CardSuit.Spade, Number = 1 });
            CardView card2 = new CardView(null, new Card { Suit = CardSuit.Heart, Number = 1 });
            CardView card3 = new CardView(null, new Card { Suit = CardSuit.Diamond, Number = 1 });
            CardView card4 = new CardView(null, new Card { Suit = CardSuit.Club, Number = 1 });
            Assert.IsTrue(card1.IsBlack());
            Assert.IsTrue(card2.IsRed());
            Assert.IsTrue(card3.IsRed());
            Assert.IsTrue(card4.IsBlack());


            Assert.IsFalse(card1.IsRed());
            Assert.IsFalse(card2.IsBlack());
            Assert.IsFalse(card3.IsBlack());
            Assert.IsFalse(card4.IsRed());
        }

        [Test]
        public void NeedByOthers()
        {            
            Foundations foundations = new Foundations(null);
            Tableau tableau = new Tableau(null);
            tableau.GetColumn(0).AddCards("h3");
            foundations.GetColumn(0).AddCards("s2");
            Assert.IsTrue(tableau.GetColumn(0).GetLastCard().NeededByOthers(tableau, foundations));
        }
    }
}