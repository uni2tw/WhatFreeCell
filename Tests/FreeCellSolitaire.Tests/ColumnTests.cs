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

            card.Move(tableau.GetColumn(0));
            
            Assert.IsTrue(card.Move(tableau.GetColumn(1)));
            Assert.AreEqual(tableau.GetColumn(1).GetLastCard(), card);
            Assert.AreEqual(card.Owner.Owner, tableau);
        }
        [Test]
        public void MoveFromTableauToTableauMultiCards()
        {

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

        

    }
}