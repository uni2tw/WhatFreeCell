using FreeCellSolitaire.Core.CardModels;
using FreeCellSolitaire.Core.GameModels;
using FreeCellSolitaire.Entities.GameEntities;
using FreeCellSolitaire.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Reflection.Metadata.Ecma335;
using System.Windows.Forms;

namespace CoreForm.UI
{

    public delegate void GameEventHandler();

    /// <summary>
    /// UI抽象層，邏輯,事件與互動
    /// </summary>
    public interface IGameUI
    {
        void InitScreen(int ratio);
        void Start(int? deckNo);        
        void InitEvents();
        void Move(string notation);
        
        void StartGame();
        void PickNumberStartGame();
        void RestartGame();        
        void QuitGame();
        bool QuitGameConfirm();
        void AbouteGame();
        int CreateScripts(out Queue<string> scripts);

        public int? GameNumber { get; set; }
    }


    public class GameUI : IGameUI
    {
        IGameForm _form;
        DialogManager _dialog;
        IGame _game;
        private TableauContainer _tableauUI;
        private HomecellsContainer _homecellsUI;
        private FoundationsContainer _foundationsUI;

        int _defaultWidth = 800;
        int _defaultHeight = 600;
        int _ratio;

        public GameUI(IGameForm form, DialogManager dialog)
        {
            this._form = form;
            this._dialog = dialog;
            this._game = new Game { EnableAssist = true };
        }

        public void InitEvents()
        {
           
        }

        /// <summary>
        /// 初始空畫面
        /// </summary>
        /// <param name="menuHeight"></param>
        public void InitScreen(int ratio)
        {
            int width = _defaultWidth * ratio / 100;
            int height = _defaultHeight * ratio / 100;
            _ratio = ratio;
            //空畫面            
            InitBoardScreen(width, height);

            InitControls();
        }

        private int _layoutMarginTop = 24;
        private int _cardWidth;
        private int _cardHeight;
        private void InitBoardScreen(int boardWidth, int boardHeight)
        {
            _form.Width = boardWidth;
            _form.Height = boardHeight;
            _form.BackColor = Color.FromArgb(0, 123, 0);
  
            _cardWidth = (int)(Math.Floor((decimal)_form.ClientSize.Width / 9));
            _cardHeight = (int)(_cardWidth * 1.38);
        }

        private void InitControls()
        {        
            int left = 0;            
            int top = _layoutMarginTop;
            int right = _form.ClientSize.Width;
            int bottom = _form.ClientSize.Height;
            Rectangle rect = new Rectangle(left, top, _form.ClientSize.Width, _form.ClientSize.Height);
            this._foundationsUI = new FoundationsContainer(
                columnNumber: 4, rect, dock: 1 , _cardWidth, _cardHeight);

            this._form.SetControlReady(this._foundationsUI);
            
            this._homecellsUI = new HomecellsContainer(
                columnNumber: 4, rect, dock: 2,_cardWidth, _cardHeight);

            this._form.SetControlReady(this._homecellsUI);

            this._tableauUI = new TableauContainer(
                columnNumber: 8, rect, dock: 3, _cardWidth, _cardHeight);

            this._form.SetControlReady(this._tableauUI);

        }

        public void Move(string notation)
        {
            _game.Move(notation);
            Redraw();            
            CheckStatus();
        }

        public void Start(int? deckNo)
        {
            _game = new Game { EnableAssist = true };
            var tableau = new Tableau(_game);
            var homecells = new Homecells(_game);
            var foundations = new Foundations(_game);
          
            var deck = _game.Deck.Shuffle(deckNo);
            tableau.Init(deck);
            this.GameNumber = deck.Number;

            _tableauUI.Clear();
            _homecellsUI.Clear();
            _foundationsUI.Clear();

            this.Redraw();

            _form.SetCaption(this.GameNumber.ToString());
        }
        
        private void CheckStatus()
        {
            GameStatus stats = _game.EstimateGameover(false);
            if (stats == GameStatus.Completed)
            {
                var choice = _dialog.ShowYouWinContinueDialog(210 * _ratio / 100);
                if (choice.Reuslt == DialogResult.Yes)
                {
                    if (choice.CheckedYes)
                    {
                        PickNumberStartGame();
                    }
                    else
                    {
                        StartGame();
                    }
                }
            } 
        }

        public int? GameNumber { get; set; }

        private void Redraw()
        {
            RedrawTableau();
            RedrawHomecells();
            RedrawFoundations();
        }


        private void RedrawTableau()
        {
            var _tableau = _game.Tableau;
            for (int columnIndex = 0; columnIndex < _tableau.ColumnCount; columnIndex++)
            {
                List<Card> cards = new List<Card>();
                for (int cardIndex = 0; cardIndex < _tableau.GetColumn(columnIndex).GetCardsCount(); cardIndex++)
                {
                    cards.Add(new Card
                    {
                        Number = _tableau.GetColumn(columnIndex).GetCard(cardIndex).Number,
                        Suit = _tableau.GetColumn(columnIndex).GetCard(cardIndex).Suit
                    });
                }
                _tableauUI.RedrawCards(columnIndex, cards);
            }
        }

        private void RedrawFoundations()
        {
            var _foundations = _game.Foundations;
            for (int columnIndex = 0; columnIndex < _foundations.ColumnCount; columnIndex++)
            {
                List<Card> cards = new List<Card>();
                for (int cardIndex = 0; cardIndex < _foundations.GetColumn(columnIndex).GetCardsCount(); cardIndex++)
                {
                    cards.Add(new Card
                    {
                        Number = _foundations.GetColumn(columnIndex).GetCard(cardIndex).Number,
                        Suit = _foundations.GetColumn(columnIndex).GetCard(cardIndex).Suit
                    });
                }
                _foundationsUI.RedrawCards(columnIndex, cards);
            }
        }

        private void RedrawHomecells()
        {
            var _homecells = _game.Homecells;
            for (int columnIndex = 0; columnIndex < _homecells.ColumnCount; columnIndex++)
            {
                List<Card> cards = new List<Card>();
                for (int cardIndex = 0; cardIndex < _homecells.GetColumn(columnIndex).GetCardsCount(); cardIndex++)
                {
                    cards.Add(new Card
                    {
                        Number = _homecells.GetColumn(columnIndex).GetCard(cardIndex).Number,
                        Suit = _homecells.GetColumn(columnIndex).GetCard(cardIndex).Suit
                    });
                }
                _homecellsUI.RedrawCards(columnIndex, cards);
            }
        }

        public void StartGame()
        {
            if (_game.IsPlaying() &&
                MessageBox.Show("是否放棄這一局", "新接龍", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                return;
            }
            this.Start(null);            
        }

        public void PickNumberStartGame()
        {
            var gameNumber = _game.Deck.GetRandom();
            var dialogResult = _dialog.ShowSelectGameNumberDialog(210 * _ratio / 100, gameNumber);
            if (dialogResult.Reuslt == DialogResult.Yes)
            {
                Start(int.Parse(dialogResult.ReturnText));
                Redraw();
            }
        }

        public void RestartGame()
        {
            if (MessageBox.Show("是否放棄這一局", "新接龍", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                return;
            }
            Start(GameNumber);
        }


        public bool QuitGameConfirm()
        {
            if (_game.IsPlaying() &&
                MessageBox.Show("是否放棄這一局", "新接龍", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                return false;
            }
            return true;
        }


        public void QuitGame()
        {
            _form.Close();
        }

        public void AbouteGame()
        {
            MessageBox.Show("2022的計劃");
        }

        public int CreateScripts(out Queue<string> scripts)
        {
            scripts = new Queue<string>();
            scripts.Enqueue("t1h0");
            scripts.Enqueue("t2t0");
            scripts.Enqueue("t7t3");
            scripts.Enqueue("t7t1");
            scripts.Enqueue("t7f0");
            scripts.Enqueue("t7t2");
            scripts.Enqueue("t7f1");
            scripts.Enqueue("t6f2");
            scripts.Enqueue("t2h3");
            scripts.Enqueue("t1h3");
            scripts.Enqueue("t2f0");
            scripts.Enqueue("f2t7");
            scripts.Enqueue("t2t7");
            scripts.Enqueue("t2t3");
            scripts.Enqueue("t2t5");
            scripts.Enqueue("t5f2");
            scripts.Enqueue("t5t7");
            scripts.Enqueue("f2t7");
            scripts.Enqueue("t0h2");
            scripts.Enqueue("t6f2");
            scripts.Enqueue("t5f3");
            scripts.Enqueue("t5t2");
            scripts.Enqueue("t5t2");
            scripts.Enqueue("t5h2");
            scripts.Enqueue("t4f0");
            scripts.Enqueue("t4t1");
            scripts.Enqueue("f2t5");
            scripts.Enqueue("t4f2");
            scripts.Enqueue("t4t5");
            scripts.Enqueue("t4t2");
            scripts.Enqueue("t6t2");
            scripts.Enqueue("f0t4");
            scripts.Enqueue("t0t4");
            scripts.Enqueue("t0t4");
            scripts.Enqueue("t0t6");
            scripts.Enqueue("f2h1");
            scripts.Enqueue("f3h2");
            scripts.Enqueue("f1t6");
            scripts.Enqueue("t3t4");
            scripts.Enqueue("t3h2");
            scripts.Enqueue("t3t2");
            scripts.Enqueue("t3f0");
            scripts.Enqueue("t3f1");
            scripts.Enqueue("t1f0");
            scripts.Enqueue("t1f1");
            return 1;            
        }
    }

}
