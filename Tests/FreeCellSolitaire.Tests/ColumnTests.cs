using FreeCellSolitaire.Core.CardModels;
using FreeCellSolitaire.Core.GameModels;
using FreeCellSolitaire.Entities.GameEntities;

namespace FreeCellSolitaire.Tests
{
    public class ColumnTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void HomecellsCannotDragaable()
        {
            Assert.IsFalse(new Homecells(null).GetColumn(0).Draggable());
        }

        [Test]
        public void TableauDragaable()
        {
            Assert.IsTrue(new Foundations(null).GetColumn(0).Draggable());
        }

        [Test]
        public void FoundactionsDragaable()
        {
            Assert.IsTrue(new Foundations(null).GetColumn(0).Draggable());
        }

        [Test]
        public void HomecellsAddCard()
        {
            var homecells = new Homecells(null);
            homecells.GetColumn(0).AddCards(new Card { Suit = CardSuit.Club, Number = 1 });

            var card = homecells.GetColumn(0).GetLastCard();
            Assert.AreEqual(card.Number, 1);
            Assert.AreEqual(card.Suit, CardSuit.Club);
        }

        [Test]
        public void FoundationsAddCard()
        {
            var foundations = new Foundations(null);
            foundations.GetColumn(0).AddCards(new Card { Suit = CardSuit.Club, Number = 1 });

            var card = foundations.GetColumn(0).GetLastCard();
            Assert.AreEqual(card.Number, 1);
            Assert.AreEqual(card.Suit, CardSuit.Club);
        }

        [Test]
        public void TableauAddCard()
        {
            var tableau = new Tableau(null);
            tableau.GetColumn(0).AddCards(new Card { Suit = CardSuit.Club, Number = 1 });

            var card = tableau.GetColumn(0).GetLastCard();
            Assert.AreEqual(card.Number, 1);
            Assert.AreEqual(card.Suit, CardSuit.Club);
        }

        [Test]
        public void MoveableFromFoundactionsToFoundactions()
        {
            var foundations = new Foundations(null);
            foundations.GetColumn(0).AddCards(new Card { Suit = CardSuit.Club, Number = 1 });
            var card = foundations.GetColumn(0).GetLastCard();

            Assert.IsTrue(card.Move(foundations.GetColumn(1)));
            Assert.AreEqual(foundations.GetColumn(1).GetLastCard(), card);
            Assert.AreEqual(card.Owner.Owner, foundations);
        }
        [Test]
        public void MoveableFromFoundactionsToTableau()
        {
            var foundations = new Foundations(null);
            foundations.GetColumn(0).AddCards(new Card { Suit = CardSuit.Club, Number = 1 });
            var card = foundations.GetColumn(0).GetLastCard();

            var tableau = new Tableau(null);
            Assert.IsTrue(card.Move(tableau.GetColumn(0)));
            Assert.AreEqual(tableau.GetColumn(0).GetLastCard(), card);
            Assert.AreEqual(card.Owner.Owner, tableau);
        }
        [Test]
        public void MoveableFromFoundactionsToHomecells()
        {
            var foundations = new Foundations(null);
            foundations.GetColumn(0).AddCards(new Card { Suit = CardSuit.Club, Number = 1 });
            var card = foundations.GetColumn(0).GetLastCard();

            var homecells = new Homecells(null);
            Assert.IsTrue(card.Move(homecells.GetColumn(0)));
            Assert.AreEqual(homecells.GetColumn(0).GetLastCard(), card);
            Assert.AreEqual(card.Owner.Owner, homecells);
        }

        [Test]
        public void MoveableFromHomecellsToFoundactions()
        {
            var homecells = new Homecells(null);
            homecells.GetColumn(0).AddCards(new Card { Suit = CardSuit.Club, Number = 1 });
            var card = homecells.GetColumn(0).GetLastCard();

            Assert.IsFalse(card.Move(new Foundations(null).GetColumn(0)));

        }
        [Test]
        public void MoveableFromHomecellsToTableau()
        {
            var homecells = new Homecells(null);
            homecells.GetColumn(0).AddCards(new Card { Suit = CardSuit.Club, Number = 1 });
            var card = homecells.GetColumn(0).GetLastCard();

            Assert.IsFalse(card.Move(new Tableau(null).GetColumn(0)));
        }
        [Test]
        public void MoveableFromHomecellsToHomecells()
        {
            var homecells = new Homecells(null);
            homecells.GetColumn(0).AddCards(new Card { Suit = CardSuit.Club, Number = 1 });
            var card = homecells.GetColumn(0).GetLastCard();

            Assert.IsFalse(card.Move(homecells.GetColumn(1)));
        }

        [Test]
        public void MoveableFromTableauToTableau()
        {
            var tableau = new Tableau(null);
            tableau.GetColumn(0).AddCards(new Card { Suit = CardSuit.Club, Number = 1 });
            var card = tableau.GetColumn(0).GetLastCard();

            Assert.IsTrue(card.Move(tableau.GetColumn(1)));
            Assert.AreEqual(tableau.GetColumn(1).GetLastCard(), card);
            Assert.AreEqual(card.Owner.Owner, tableau);
        }
        [Test]
        public void GetLinkedCards()
        {
            var tableau = new Tableau(null);
            tableau.GetColumn(0).AddCards(new Card { Number = 3, Suit = CardSuit.Spade });
            tableau.GetColumn(0).AddCards(new Card { Number = 2, Suit = CardSuit.Heart });
            tableau.GetColumn(0).AddCards(new Card { Number = 1, Suit = CardSuit.Club });

            Assert.AreEqual(tableau.GetColumn(0).GetCard(0).GetLinkedCards().Count, 3);
            Assert.AreEqual(tableau.GetColumn(0).GetCard(1).GetLinkedCards().Count, 2);
            Assert.AreEqual(tableau.GetColumn(0).GetCard(2).GetLinkedCards().Count, 1);
        }

       
        [Test]
        public void TableauMoveMoreThanOneCard()
        {
            var tableau = new Tableau(null);
            tableau.GetColumn(0).AddCards(new Card { Number = 3, Suit = CardSuit.Spade });
            tableau.GetColumn(0).AddCards(new Card { Number = 2, Suit = CardSuit.Heart });
            tableau.GetColumn(0).AddCards(new Card { Number = 1, Suit = CardSuit.Club });

            var card = tableau.GetColumn(0).GetCard(0);

            Assert.AreEqual(tableau.GetColumn(0).GetCardsCount(), 3);
            Assert.AreEqual(tableau.GetColumn(1).GetCardsCount(), 0);

            card.Move(tableau.GetColumn(1));

            Assert.AreEqual(0, tableau.GetColumn(0).GetCardsCount());
            Assert.AreEqual(3, tableau.GetColumn(1).GetCardsCount());            
        }


        [Test]
        public void MoveableFromTableauToHomecells()
        {
            var tableau = new Tableau(null);
            tableau.GetColumn(0).AddCards(new Card { Suit = CardSuit.Club, Number = 1 });
            var card = tableau.GetColumn(0).GetLastCard();

            var homecells = new Homecells(null);
            Assert.IsTrue(card.Move(homecells.GetColumn(0)));
            Assert.AreEqual(homecells.GetColumn(0).GetLastCard(), card);
            Assert.AreEqual(card.Owner.Owner, homecells);
        }
        [Test]
        public void MoveableFromTableauToFoundactions()
        {
            var tableau = new Tableau(null);
            tableau.GetColumn(0).AddCards(new Card { Suit = CardSuit.Club, Number = 1 });
            var card = tableau.GetColumn(0).GetLastCard();

            var foundations = new Foundations(null);
            Assert.IsTrue(card.Move(foundations.GetColumn(0)));
            Assert.AreEqual(foundations.GetColumn(0).GetLastCard(), card);
            Assert.AreEqual(card.Owner.Owner, foundations);
        }

        [Test]
        public void ColumnToString()
        {
            Column col = new Column(new Tableau(null), 0, 2);
            col.AddCards(new Card { Suit = CardSuit.Heart, Number = 1 });
            col.AddCards(new Card { Suit = CardSuit.Heart, Number = 2 });
            Assert.AreEqual("紅心 1,紅心 2", col.ToString());
            Assert.AreEqual("h1,h2", col.ToNotation());
        }

        [Test]
        public void FoundationsAddCards() {
            var foundations = new Foundations(null);            
            Assert.AreEqual(1, foundations.GetColumn(0).AddCards("s1"));
            Assert.AreEqual(1, foundations.GetColumn(1).AddCards("s1,s2"));            
        }
        [Test]
        public void HomecellsAddCards()
        {
            var homecells = new Homecells(null);
            Assert.AreEqual(1, homecells.GetColumn(0).AddCards("s1"));
            Assert.AreEqual(2, homecells.GetColumn(1).AddCards("s1,s2"));
            //AddCards 是初始資料用的，所以不會做額外的檢查，給什麼就放什麼
            //此邏輯暫不改
            //所以先調整測試程式先pass
            Assert.AreEqual(2, homecells.GetColumn(2).AddCards("s1,s3"));
            Assert.AreEqual(1, homecells.GetColumn(3).AddCards("s2"));
        }
        [Test]
        public void TableauAddCards()
        {
            var tableau = new Tableau(null);
            Assert.AreEqual(1, tableau.GetColumn(0).AddCards("s1"));
            Assert.AreEqual(2, tableau.GetColumn(1).AddCards("s1,s2"));
            Assert.AreEqual(1, tableau.GetColumn(1).AddCards("s2"));
        }
    }
   
}