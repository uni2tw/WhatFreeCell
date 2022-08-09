using FreeCellSolitaire.Core.CardModels;
using FreeCellSolitaire.Core.GameModels;
using FreeCellSolitaire.Entities.GameEntities;

namespace FreeCellSolitaire.Tests
{
    public class TableauTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void _01_Init()
        {
            var deck = Deck.Create().Shuffle(101);
            var tableau = new Tableau(null);
            tableau.Init(deck);
            tableau.DebugInfo();
        }

        [Test]
        public void _02_GetCard()
        {
            var deck = Deck.Create().Shuffle(101);
            var tableau = new Tableau(null);
            tableau.Init(deck);            

            CardView card = tableau.GetColumn(0).GetLastCard();
            Console.WriteLine($"card:{card}");
            Assert.IsNotNull(card);
        }


        [Test]
        public void _03_Moveable()
        {
            var deck = Deck.Create(1).Shuffle(101);
            var tableau = new Tableau(null);
            var homecells = new Homecells(null);
            
            tableau.GetColumn(0).AddCards(new Card { Suit = CardSuit.Spade, Number = 1 });

            var card = tableau.GetColumn(0).GetLastCard();
            

            Assert.IsTrue(card.Moveable(homecells.GetColumn(0)));
         
        }


        [Test]
        public void _04_MoveCard()
        {
            var deck = Deck.Create().Shuffle(101);
            var tableau = new Tableau(null);
            tableau.Init(deck);


            CardView card = tableau.GetColumn(5).GetLastCard();
            Assert.AreEqual(card.Suit, CardSuit.Heart);
            Assert.AreEqual(card.Number, 12);

            CardView card2 = tableau.GetColumn(3).GetLastCard();
            Assert.AreEqual(card2.Suit, CardSuit.Spade);
            Assert.AreEqual(card2.Number, 13);


            tableau.DebugColumnInfo(3);
            tableau.DebugColumnInfo(5);
            Assert.IsTrue(card.Move(tableau.GetColumn(3)));
            tableau.DebugColumnInfo(3);
            tableau.DebugColumnInfo(5);         
        }



        [Test]
        public void _04_MoveCardExpectFalse()
        {
            var deck = Deck.Create().Shuffle(101);
            var tableau = new Tableau(null);
            tableau.Init(deck);


            CardView card = tableau.GetColumn(5).GetLastCard();
            Assert.AreEqual(card.Suit, CardSuit.Heart);
            Assert.AreEqual(card.Number, 12);

            CardView card2 = tableau.GetColumn(3).GetLastCard();
            Assert.AreEqual(card2.Suit, CardSuit.Spade);
            Assert.AreEqual(card2.Number, 13);



            tableau.DebugColumnInfo(3);
            tableau.DebugColumnInfo(5);
            //TODO ¥ý´úmoveable
            Assert.IsFalse(card2.Move(tableau.GetColumn(5)));
            tableau.DebugColumnInfo(3);
            tableau.DebugColumnInfo(5);
        }

        [Test]
        public void _05_CheckEmpty()
        {
            var deck = Deck.Create(0).Shuffle(101);
            var tableau = new Tableau(null);
            tableau.Init(deck);
            Assert.IsTrue(tableau.WasEmpty());

            deck = Deck.Create(1).Shuffle(101);
            tableau = new Tableau(null);
            tableau.Init(deck);
            Assert.IsFalse(tableau.WasEmpty());
           
        }

        [Test]
        public void Clone()
        {
            var tableau = new Tableau(null);
            tableau.GetColumn(0).AddCards("h1,h2");
            Assert.AreEqual("h1,h2", tableau.GetColumn(0).ToNotation());
            var clone = tableau.Clone() as Tableau;
            Assert.AreEqual("h1,h2", clone.GetColumn(0).ToNotation());
        }

    }
}