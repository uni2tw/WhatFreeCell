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
        void Redraw();
        void Move(string notation);

        public int? GameNumber { get; set; }
    }

    public class GameUI : IGameUI
    {
        IGameForm _form;
        IGame _game;
        private TableauContainer _tableauUI;
        private HomecellsContainer _homecellsUI;
        private FoundationsContainer _foundationsUI;

        int _defaultWidth = 800;
        int _defaultHeight = 600;
        int _ratio;

        public GameUI(IGameForm form)
        {
            this._form = form;            
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
            //功能表單
            InitializeMenu();

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

        /// <summary>
        /// 初始化功能表單
        /// </summary>
        private void InitializeMenu()
        {
            var self = this;

            var menu = new System.Windows.Forms.MenuStrip();

            menu.Dock = DockStyle.Top;
            menu.BackColor = Color.Silver;


            var menuItem0 = new ToolStripMenuItem();
            menuItem0.Text = "遊戲(&G)";
            menu.Items.Add(menuItem0);
            menuItem0.DropDownItems.Add("新遊戲", null).Click += delegate (object sender, EventArgs e)
            {
                if (_game.IsPlaying() &&                
                    MessageBox.Show("是否放棄這一局", "新接龍", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    return;
                }
                this.Start(null);
                this.Redraw();
                _form.SetCaption(this.GameNumber.ToString());
            };
            menuItem0.DropDownItems.Add("選擇牌局", null).Click += delegate (object sender, EventArgs e)
            {
                _form.ShowSelectGameNumberDialog(_game.Deck.GetRandom());
            };
            menuItem0.DropDownItems.Add("重啟牌局", null).Click += delegate (object sender, EventArgs e)
            {
                if (MessageBox.Show("是否放棄這一局", "新接龍", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    return;
                }
                Start(GameNumber);
                Redraw();
            };
            menuItem0.DropDownItems.Add(new ToolStripSeparator());
            menuItem0.DropDownItems.Add("結束(&X)", null).Click += delegate (object sender, EventArgs e)
            {
                if (_game.IsPlaying() &&
                    MessageBox.Show("是否放棄這一局", "新接龍", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    return;
                }
                _form.Quit();
            };

            var menuItem1 = new ToolStripMenuItem();
            menuItem1.Text = "說明(&H))";
            menu.Items.Add(menuItem1);
            menuItem1.DropDownItems.Add("關於新接龍", null).Click += delegate (object sender, EventArgs e)
            {
                
            };

            _form.SetControl(menu);
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
            CheckCompleted();
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
        
        }

        private void CheckCompleted()
        {
            
        }

        public int? GameNumber { get; set; }

        public void Redraw()
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
    }

}
