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

        //[Test]
        //public void Tableau_internal_moveable_test()
        //{
        //    var tableau = new Tableau(null);
        //    tableau.Init(null);
        //    CardView card = new CardView(tableau.GetColumn(0), new Card { Number = 1, Suit = CardSuit.Spade });

        //    CardView card2 = new CardView(tableau.GetColumn(1), new Card { Number = 2, Suit = CardSuit.Heart });

        //    Assert.IsTrue(card.CheckMoveable(card2));

        //    Assert.IsFalse(card2.CheckMoveable(card));

        //    //Assert.IsFalse(card2.CardIsMoveable(tableau.GetColumn(5)));

        //}

        //[Test]
        //public void s04_Moveable()
        //{            
        //    //¶Â®ç1
        //    CardView card = new CardView(null , new Card { Number = 1, Suit = CardSuit.Spade });
        //    //¶Â®ç2
        //    CardView card2 = new CardView(null, new Card { Number = 2, Suit = CardSuit.Spade });
        //    //¬õ¤ß2
        //    CardView card3 = new CardView(null, new Card { Number = 2, Suit = CardSuit.Heart });

        //    Assert.IsTrue(card.CheckMoveable(new Homecells(null).GetColumn(0)));
        //    Assert.IsFalse(card.CheckMoveable(card3, new Homecells(null)));


        //}

    }
}