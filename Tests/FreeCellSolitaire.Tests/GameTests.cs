using System.Linq;
using FreeCellSolitaire.Core.CardModels;
using FreeCellSolitaire.Core.GameModels;
using FreeCellSolitaire.Entities.GameEntities;

namespace FreeCellSolitaire.Tests
{
    public class GameTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void GameExtraMobility()
        {
            IGame game = new Game();
            var tableau = new Tableau(game);
            var homecells = new Homecells(game);
            var foundations = new Foundations(game);
            Assert.AreEqual(12, game.GetExtraMobility());

            tableau.GetColumn(0).AddCards(new Card { Number = 1, Suit = CardSuit.Club });
            Assert.AreEqual(11, game.GetExtraMobility());
            tableau.GetColumn(1).AddCards(new Card { Number = 2, Suit = CardSuit.Club });
            Assert.AreEqual(10, game.GetExtraMobility());
            tableau.GetColumn(2).AddCards(new Card { Number = 3, Suit = CardSuit.Club });
            Assert.AreEqual(9, game.GetExtraMobility());
            tableau.GetColumn(3).AddCards(new Card { Number = 4, Suit = CardSuit.Club });
            Assert.AreEqual(8, game.GetExtraMobility());
            tableau.GetColumn(4).AddCards(new Card { Number = 5, Suit = CardSuit.Club });
            Assert.AreEqual(7, game.GetExtraMobility());
            tableau.GetColumn(5).AddCards(new Card { Number = 6, Suit = CardSuit.Club });
            Assert.AreEqual(6, game.GetExtraMobility());
            tableau.GetColumn(6).AddCards(new Card { Number = 7, Suit = CardSuit.Club });
            Assert.AreEqual(5, game.GetExtraMobility());
            tableau.GetColumn(7).AddCards(new Card { Number = 8, Suit = CardSuit.Club });
            Assert.AreEqual(4, game.GetExtraMobility());

            foundations.GetColumn(0).AddCards(new Card { Number = 9, Suit = CardSuit.Club });
            Assert.AreEqual(3, game.GetExtraMobility());
            foundations.GetColumn(1).AddCards(new Card { Number = 10, Suit = CardSuit.Club });
            Assert.AreEqual(2, game.GetExtraMobility());
            foundations.GetColumn(2).AddCards(new Card { Number = 11, Suit = CardSuit.Club });
            Assert.AreEqual(1, game.GetExtraMobility());
            foundations.GetColumn(3).AddCards(new Card { Number = 12, Suit = CardSuit.Club });
            Assert.AreEqual(0, game.GetExtraMobility());

        }

        [Test]
        public void GameExtraMobilityToMove()
        {
            IGame game = new Game();
            Deck.Create().Shuffle(1);
            var tableau = new Tableau(game);
            var homecells = new Homecells(game);
            var foundations = new Foundations(game);

            foundations.GetColumn(0).AddCards(new Card { Number = 13, Suit = CardSuit.Spade });            
            foundations.GetColumn(1).AddCards(new Card { Number = 13, Suit = CardSuit.Heart });            
            foundations.GetColumn(2).AddCards(new Card { Number = 13, Suit = CardSuit.Diamond });            
            foundations.GetColumn(3).AddCards(new Card { Number = 13, Suit = CardSuit.Club });

            foundations.DebugInfo();

            tableau.GetColumn(4).AddCards(new Card { Number = 12 , Suit = CardSuit.Spade });            
            tableau.GetColumn(5).AddCards(new Card { Number = 12, Suit = CardSuit.Heart });            
            tableau.GetColumn(6).AddCards(new Card { Number = 12, Suit = CardSuit.Diamond });            
            tableau.GetColumn(7).AddCards(new Card { Number = 12, Suit = CardSuit.Club });
            
            tableau.GetColumn(0).AddCards(new Card { Number = 3, Suit = CardSuit.Club });            
            tableau.GetColumn(0).AddCards(new Card { Number = 2,  Suit = CardSuit.Diamond });            
            tableau.GetColumn(0).AddCards(new Card { Number = 1, Suit = CardSuit.Club });            
            tableau.GetColumn(1).AddCards(new Card { Number = 4, Suit = CardSuit.Diamond });


            tableau.DebugInfo();

            Assert.AreEqual(2, game.GetExtraMobility());

        }
        [Test]
        public void GameMove()
        {
            IGame game = new Game();
            var tableau = new Tableau(game);         
            var homecells = new Homecells(game);
            var foundations = new Foundations(game);
            tableau.GetColumn(0).AddCards(new Card { Number = 1, Suit = CardSuit.Spade });


            Assert.AreEqual(1, tableau.GetColumn(0).GetCardsCount());
            game.DebugInfo(1);
            game.Move("t0t1");

            Assert.AreEqual(1, tableau.GetColumn(1).GetCardsCount());
            game.DebugInfo(2);
            game.Move("t1f1");

            Assert.AreEqual(1, foundations.GetColumn(1).GetCardsCount());
            game.DebugInfo(3);
            game.Move("f1f0");

            Assert.AreEqual(1, foundations.GetColumn(0).GetCardsCount());
            game.DebugInfo(4);
            game.Move("f0t0");

        }

       
        [Test]
        public void GameMoveWithAssist_01()
        {
            IGame game = new Game();
            game.EnableAssist = true;
            var tableau = new Tableau(game);
            var homecells = new Homecells(game);
            var foundations = new Foundations(game);

            tableau.GetColumn(0).AddCards("h2,h1");
            
            game.Move("t0f0");

            game.DebugInfo(1);

            Assert.AreEqual(homecells.GetColumn(0).GetLastCard().Number, 2);
        }
        [Test]
        public void GameMoveWithAssist_02()
        {
            IGame game = new Game();
            game.EnableAssist = true;
            var tableau = new Tableau(game);
            var homecells = new Homecells(game);
            var foundations = new Foundations(game);

            tableau.GetColumn(0).AddCards("h4,h2,h1,h3");
            tableau.GetColumn(1).AddCards("c4,c3,c1,c2");

            game.Move("t0f0");
            game.DebugInfo(1);
            game.Move("t1f1");
            game.DebugInfo(2);

            Assert.AreEqual("紅心 4", homecells.GetColumn(0).GetLastCard().ToString());
            Assert.AreEqual("梅花 4", homecells.GetColumn(1).GetLastCard().ToString());
        }

        [Test]
        public void GameMoveWithAssist_03()
        {
            IGame game = new Game();
            game.EnableAssist = true;
            var tableau = new Tableau(game);
            var homecells = new Homecells(game);
            var foundations = new Foundations(game);

            tableau.GetColumn(0).AddCards("h4,h2,h1,h3");
            tableau.GetColumn(1).AddCards("c4,c3,c1,c2");
            tableau.GetColumn(2).AddCards("d4,d2,d1,d3");
            tableau.GetColumn(3).AddCards("s4,s3,s1,s2");

            game.Move("t0f0");
            game.DebugInfo(1);
            game.Move("t1f1");
            game.DebugInfo(2);

            Assert.AreEqual("紅心 2", homecells.GetColumn(0).GetLastCard().ToString());
            Assert.AreEqual("梅花 2", homecells.GetColumn(1).GetLastCard().ToString());
        }


        [Test]
        public void FullGameMove()
        {
            IGame game = new Game();
            var tableau = new Tableau(game);
            var homecells = new Homecells(game);
            var foundations = new Foundations(game);
            //origin-26458
            tableau.GetColumn(0).AddCards("s13,h11,s5,c11,s8,h9,h4");
            tableau.GetColumn(1).AddCards("h12,d12,c13,d13,d5,h2,h1");
            tableau.GetColumn(2).AddCards("c2,c5,c6,s7,d4,d1,c3");
            tableau.GetColumn(3).AddCards("d8,h6,c9,s11,d9,s6,c8");
            tableau.GetColumn(4).AddCards("d11,s9,d7,h3,s12,c10");
            tableau.GetColumn(5).AddCards("c4,c12,d3,h13,c7,d6");
            tableau.GetColumn(6).AddCards("h5,s10,d2,h10,s1,h8");
            tableau.GetColumn(7).AddCards("c1,d10,s3,s2,s4,h7");


            game.DebugInfo(1);
            game.Move("t1h0");
            game.Move("t1h0");
            game.DebugInfo(2);
            game.Move("t2t0");
            game.Move("t2h1");
            game.DebugInfo(3);
            game.Move("t7t3");
            game.Move("t7t1");

            game.Move("t7f0");
            game.Move("t7t2");

            game.Move("t7f1");
            game.Move("t7h2");
            game.DebugInfo(4);

            game.Move("t6f2");
            game.Move("t6h3");
            game.Move("f0h3");
            game.DebugInfo(5);

            game.Move("t2h3");
            game.Move("t1h3");
            game.Move("t2f0");
            game.Move("f2t7");
            game.Move("t2t7");

            game.Move("t2t3");
            game.Move("t2t5");
            game.Move("t2h2");

            game.DebugInfo(6);
            game.Move("t5f2");
            game.Move("t5t7");
            game.Move("f2t7");
            game.Move("t0h2");
            game.Move("t6f2");
            game.Move("t6h1");

            game.DebugInfo(7);

            game.Move("t5f3");
            game.Move("t5t2");
            game.Move("t5h1");
            game.Move("t5t2");
            game.Move("t5h2");
            game.Move("f0h1");
            game.Move("t1h1");
            game.DebugInfo(8);

            game.Move("t4f0");
            game.Move("t4t1");
            game.Move("t4h0");
            game.Move("t0h0");
            game.Move("t7h2");
            game.Move("f2t5");
            game.Move("t4f2");
            game.Move("t4t5");
            game.Move("t4t2");
            game.Move("t6t2");
            game.Move("t6h0");
            game.Move("t3h2");

            game.DebugInfo(9);

            game.Move("f0t4");
            game.Move("t0t4");
            game.Move("t0t4");
            game.Move("t0t6");
            game.Move("t0h3");
            game.Move("t7h1");

            game.Move("f2h1");
            game.Move("f3h2");
            game.Move("f1t6");

            game.DebugInfo(10);

            game.Move("t3t4");
            game.Move("t3h2");
            game.Move("t3h3");
            game.Move("t3t2");
            game.Move("t3f0");
            game.Move("t3f1");
            game.Move("t3h0");

            game.DebugInfo(11);

            game.Move("t7h3");
            game.Move("t4h0");
            game.Move("t3h1");
            game.Move("t4h3");
            game.Move("t7h0");
            game.Move("t4h0");
            game.Move("t2h1");
            game.Move("f1h2");
            game.Move("t5h3");
            game.Move("t5h0");
            game.Move("t6h1");
            game.Move("t4h2");
            game.Move("t2h3");
            game.Move("t0h0");
            game.Move("t2h1");
            game.Move("t6h2");
            game.Move("f0h3");
            game.Move("t1h3");
            game.Move("t2h2");
            game.DebugInfo(12);

            game.Move("t1f0");
            game.Move("t1f1");
            game.Move("t1h1");
            game.Move("t1h0");
            game.Move("f0h1");            
            game.Move("f1h2");
            game.Move("t0h3");
            game.Move("t2h0");
            game.DebugInfo(13);

            Assert.IsTrue(game.IsCompleted(), "遊戲完成");

        }

        [Test]
        public void FullGameMoveWithAssist()
        {
            IGame game = new Game() { EnableAssist = true };
            var tableau = new Tableau(game);
            var homecells = new Homecells(game);
            var foundations = new Foundations(game);
            //origin-26458
            tableau.GetColumn(0).AddCards("s13,h11,s5,c11,s8,h9,h4");
            tableau.GetColumn(1).AddCards("h12,d12,c13,d13,d5,h2,h1");
            tableau.GetColumn(2).AddCards("c2,c5,c6,s7,d4,d1,c3");
            tableau.GetColumn(3).AddCards("d8,h6,c9,s11,d9,s6,c8");
            tableau.GetColumn(4).AddCards("d11,s9,d7,h3,s12,c10");
            tableau.GetColumn(5).AddCards("c4,c12,d3,h13,c7,d6");
            tableau.GetColumn(6).AddCards("h5,s10,d2,h10,s1,h8");
            tableau.GetColumn(7).AddCards("c1,d10,s3,s2,s4,h7");

            game.DebugInfo(1);
            game.Move("t1h0");

            game.DebugInfo(2);
            game.Move("t2t0");  
            
            game.DebugInfo(3);
            game.Move("t7t3");
            game.Move("t7t1");
            game.Move("t7f0");
            game.Move("t7t2");
            game.Move("t7f1");            
            game.DebugInfo(4);
           
            game.Move("t6f2");
            game.DebugInfo(5);
            Assert.AreEqual(homecells.GetColumn(3).GetLastCard().ToString(), "黑桃 2");            

            game.Move("t2h3");
            game.Move("t1h3");
            game.Move("t2f0");
            game.Move("f2t7");
            game.Move("t2t7");
            game.Move("t2t3");
            game.Move("t2t5");
            game.DebugInfo(6);

            game.Move("t5f2");
            game.Move("t5t7");
            game.Move("f2t7");
            game.Move("t0h2");
            game.Move("t6f2");
            game.DebugInfo(7);

            game.Move("t5f3");
            game.Move("t5t2");
            game.Move("t5t2");
            game.Move("t5h2");                       
            game.DebugInfo(8);
            Assert.AreEqual(homecells.GetColumn(1).GetLastCard().ToString(), "方塊 5");

            game.Move("t4f0");
            game.Move("t4t1");
            //game.Move("t4h0");
            //game.Move("t0h0");
            //game.Move("t7h2");
            game.Move("f2t5");
            game.Move("t4f2");
            game.Move("t4t5");
            game.Move("t4t2");
            game.Move("t6t2");
            //game.Move("t6h0");
            //game.Move("t3h2");
            game.DebugInfo(9);
            Assert.AreEqual(homecells.GetColumn(0).GetLastCard().ToString(), "紅心 5");
            Assert.AreEqual(homecells.GetColumn(2).GetLastCard().ToString(), "梅花 6");

            game.Move("f0t4");
            game.Move("t0t4");
            game.Move("t0t4");
            game.Move("t0t6");
            //game.Move("t0h3");
            //game.Move("t7h1");
            game.Move("f2h1");
            game.Move("f3h2");
            game.Move("f1t6");
            game.DebugInfo(10);

            game.Move("t3t4");
            game.Move("t3h2");
            //game.Move("t3h3");
            game.Move("t3t2");
            game.Move("t3f0");
            game.Move("t3f1");
            //game.Move("t3h0");

            game.DebugInfo(11);


            game.Move("t1f0");
            game.Move("t1f1");            
            game.DebugInfo(13);

            Assert.IsTrue(game.IsCompleted(), "遊戲完成");

        }
        [Test]
        public void GameIsPlaying()
        {
            IGame game = new Game() { EnableAssist = true };
            var tableau = new Tableau(game);
            var homecells = new Homecells(game);
            var foundations = new Foundations(game);

            Assert.IsFalse(game.IsPlaying());
            tableau.GetColumn(0).AddCards("s1");
            Assert.IsTrue(game.IsPlaying());
            game.Move("t0h0");
            Assert.IsFalse(game.IsPlaying());
        }

        [Test]
        public void GameCloneTest()
        {
            IGame game = new Game() { EnableAssist = true };
            var tableau = new Tableau(game);
            var homecells = new Homecells(game);
            var foundations = new Foundations(game);
            //origin-26458
            tableau.GetColumn(0).AddCards("s13,h11,s5,c11,s8,h9,h4");
            tableau.GetColumn(1).AddCards("h12,d12,c13,d13,d5,h2,h1");
            tableau.GetColumn(2).AddCards("c2,c5,c6,s7,d4,d1,c3");
            tableau.GetColumn(3).AddCards("d8,h6,c9,s11,d9,s6,c8");
            tableau.GetColumn(4).AddCards("d11,s9,d7,h3,s12,c10");
            tableau.GetColumn(5).AddCards("c4,c12,d3,h13,c7,d6");
            tableau.GetColumn(6).AddCards("h5,s10,d2,h10,s1,h8");
            tableau.GetColumn(7).AddCards("c1,d10,s3,s2,s4,h7");

            var clone = game.Clone();
            Assert.AreEqual("h7", clone.Tableau.GetColumn(7).GetLastCard().ToNotation());
            Assert.AreEqual("h5,s10,d2,h10,s1,h8", clone.Tableau.GetColumn(6).ToNotation());
        }

        [Test] 
        public void GameEquals()
        {
            IGame game = new Game() { EnableAssist = true };
            var tableau = new Tableau(game);
            var homecells = new Homecells(game);
            var foundations = new Foundations(game);
            //origin-26458
            tableau.GetColumn(0).AddCards("h1");

            IGame game2 = new Game() { EnableAssist = true };
            var tableau2 = new Tableau(game2);
            var homecells2 = new Homecells(game2);
            var foundations2 = new Foundations(game2);
            //origin-26458
            tableau2.GetColumn(0).AddCards("h1");

            IGame game3 = new Game() { EnableAssist = true };
            var tableau3 = new Tableau(game3);
            var homecells3 = new Homecells(game3);
            var foundations3 = new Foundations(game3);
            //origin-26458
            tableau3.GetColumn(0).AddCards("h3");

            Assert.AreEqual(game, game2);
            Assert.AreNotEqual(game, game3);

        }

        [Test]
        public void GameHashCode()
        {
            IGame game = new Game() { EnableAssist = true };
            var tableau = new Tableau(game);
            var homecells = new Homecells(game);
            var foundations = new Foundations(game);
            //origin-26458
            tableau.GetColumn(0).AddCards("h1");
            tableau.GetColumn(0).AddCards("h2");

            IGame game2 = new Game() { EnableAssist = true };
            var tableau2 = new Tableau(game2);
            var homecells2 = new Homecells(game2);
            var foundations2 = new Foundations(game2);
            //origin-26458
            tableau2.GetColumn(0).AddCards("h1");
            tableau2.GetColumn(0).AddCards("h2");

            IGame game3 = new Game() { EnableAssist = true };
            var tableau3 = new Tableau(game3);
            var homecells3 = new Homecells(game3);
            var foundations3 = new Foundations(game3);
            tableau3.GetColumn(0).AddCards("h1");            

            HashSet<IGame> games = new HashSet<IGame>();
            games.Add(game);
            games.Add(game2);
            Assert.AreEqual(1, games.Count);
            
            games.Add(game3);
            Assert.AreEqual(2, games.Count);
        }


        [Test]
        public void GetPossibleSituations()
        {
            IGame game = new Game() { EnableAssist = true };
            var tableau = new Tableau(game);
            var homecells = new Homecells(game);
            var foundations = new Foundations(game);
            //origin-26458
            tableau.GetColumn(0).AddCards("s13,h11,s5,c11,s8,h9,h4");
            tableau.GetColumn(1).AddCards("h12,d12,c13,d13,d5,h2,h1");
            tableau.GetColumn(2).AddCards("c2,c5,c6,s7,d4,d1,c3");
            tableau.GetColumn(3).AddCards("d8,h6,c9,s11,d9,s6,c8");
            tableau.GetColumn(4).AddCards("d11,s9,d7,h3,s12,c10");
            tableau.GetColumn(5).AddCards("c4,c12,d3,h13,c7,d6");
            tableau.GetColumn(6).AddCards("h5,s10,d2,h10,s1,h8");
            tableau.GetColumn(7).AddCards("c1,d10,s3,s2,s4,h7");

            int depth = 0;
            var deductions = (game as Game).GetPossibleSituations(game, ref depth);

            game.DebugInfo(0);
            for (int i = 0; i < deductions.Count; i++)
            {
                Assert.AreNotEqual(game, deductions[i], "推演的版本，不應該跟初始版本內容相同");
                deductions[i].DebugInfo(i + 1);
            }
        }

        [Test]
        public void EstimateGameover()
        {
            IGame game = new Game() { EnableAssist = true };
            var tableau = new Tableau(game);
            var homecells = new Homecells(game);
            var foundations = new Foundations(game);
            //origin-26458
            tableau.GetColumn(0).AddCards("s13,h11,s5,c11,s8,h9,h4");
            tableau.GetColumn(1).AddCards("h12,d12,c13,d13,d5,h2,h1");
            tableau.GetColumn(2).AddCards("c2,c5,c6,s7,d4,d1,c3");
            tableau.GetColumn(3).AddCards("d8,h6,c9,s11,d9,s6,c8");
            tableau.GetColumn(4).AddCards("d11,s9,d7,h3,s12,c10");
            tableau.GetColumn(5).AddCards("c4,c12,d3,h13,c7,d6");
            tableau.GetColumn(6).AddCards("h5,s10,d2,h10,s1,h8");
            tableau.GetColumn(7).AddCards("c1,d10,s3,s2,s4,h7");

            game.DebugInfo(1);
            game.Move("t1h0");
            game.Move("t1f0");
            game.Move("t1f1");
            game.Move("t1f2");
            game.Move("t1f3");
            game.DebugInfo(2);
            Assert.AreEqual(GameStatus.Playable, game.EstimateGameover(debug: false));


            game.Move("t7t3");
            game.DebugInfo(3);

            Assert.AreEqual(GameStatus.Checkmate, game.EstimateGameover(debug: true));
        }

        [Test]
        public void EstimateGameover2()
        {
            IGame game = new Game() { EnableAssist = true };
            var tableau = new Tableau(game);
            var homecells = new Homecells(game);
            var foundations = new Foundations(game);
            //origin-26458
            foundations.GetColumn(0).AddCards("d10");
            foundations.GetColumn(1).AddCards("h8");
            foundations.GetColumn(2).AddCards("h10");
            foundations.GetColumn(3).AddCards("d4");

            tableau.GetColumn(0).AddCards("s13,h11,s5,c11,s8,h9,h4,c3");
            tableau.GetColumn(1).AddCards("h12,d12,c13,d13,d5");
            tableau.GetColumn(2).AddCards("c2,c5,c6,s7");
            tableau.GetColumn(3).AddCards("d8,h6,c9,s11,d9,s6,c8,h7");
            tableau.GetColumn(4).AddCards("d11,s9,d7,h3,s12,c10");
            tableau.GetColumn(5).AddCards("c4,c12,d3,h13,c7,d6");
            tableau.GetColumn(6).AddCards("h5");
            tableau.GetColumn(7).AddCards("s10");

            homecells.GetColumn(0).AddCards("h1,h2");
            homecells.GetColumn(1).AddCards("d1,d2");
            homecells.GetColumn(2).AddCards("s1,s2,s3,s4");
            homecells.GetColumn(3).AddCards("c1");

            game.DebugInfo(0);
            Assert.AreEqual(GameStatus.Checkmate, game.EstimateGameover(debug: true));
        }
    }
}