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
        void Reset(int deckNo);
        void Start();
        void InitEvents();
        void Redraw();
        void Move(string notation);

        bool IsPlaying { get; }
    }


    public class GameUI : IGameUI
    {
        IGameForm _form;
        IGame _game;
        private TableauContainer _tableauUI;
        private HomecellsContainer _homecellsUI;
        private FoundationsContainer _foundationsUI;

        TableauBinder _tableauBinder;
        HomecellsBinder _homecellsBinder;
        FoundationBinder _foundationBinder;

        int _defaultWidth = 800;
        int _defaultHeight = 600;
        int _ratio;

        public GameUI(IGameForm form)
        {
            this._form = form;            
        }
        public bool IsPlaying
        {
            get
            {
                return true;
            }
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
                if (this.IsPlaying &&
                    MessageBox.Show("是否放棄這一局", "新接龍", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    return;
                }
                //this.Init();
            };
            menuItem0.DropDownItems.Add("選擇牌局", null).Click += delegate (object sender, EventArgs e)
            {
                _form.ShowSelectGameNumberDialog();
            };
            menuItem0.DropDownItems.Add("重啟牌局", null).Click += delegate (object sender, EventArgs e)
            {
                _form.RestartGame();
            };
            menuItem0.DropDownItems.Add(new ToolStripSeparator());
            menuItem0.DropDownItems.Add("結束(&X)", null).Click += delegate (object sender, EventArgs e)
            {
                _form.Quit();
            };

            var menuItem1 = new ToolStripMenuItem();
            menuItem1.Text = "說明(&H))";
            menu.Items.Add(menuItem1);
            menuItem1.DropDownItems.Add("關於新接龍", null);

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
                this._form, columnNumber: 4, rect, dock: 1 , _cardWidth, _cardHeight);

            this._form.SetControlReady(this._foundationsUI);

            
            this._homecellsUI = new HomecellsContainer(
                this._form, columnNumber: 4, rect, dock: 2,_cardWidth, _cardHeight);

            this._tableauUI = new TableauContainer(
                this._form, columnNumber: 8, rect, dock: 3, _cardWidth, _cardHeight);
            //this._form.SetControlReady(this._homecellsUI);

            //        waitZone.Init(cardWidth, cardHeight, left , top, marginLeft, marginTop);
            //        waitZone.HolderClick += delegate (ColumnType zoneType, Slot slot)



        }

        public void Redraw()
        {            
            _tableauBinder.Redraw();
            _homecellsBinder.Redraw();
            _foundationBinder.Redraw();
        }

        public void Move(string notation)
        {
            _game.Move(notation);
            Redraw();
            CheckCompleted();
        }

        public void Reset(int deckNo)
        {
            _game = new Game { EnableAssist = true };
            var tableau = new Tableau(_game);
            var homecells = new Homecells(_game);
            var foundations = new Foundations(_game);
            var deck = Deck.Create().Shuffle(deckNo);
            tableau.Init(deck);

            _tableauBinder = new TableauBinder(tableau, this._tableauUI);
            _homecellsBinder = new HomecellsBinder(homecells, this._homecellsUI);
            _foundationBinder = new FoundationBinder(foundations, this._foundationsUI);
        }

        private void CheckCompleted()
        {
            
        }

        public void Start()
        {
        
        }
    }

    public class FoundationBinder
    {
        private Foundations _foundations;
        private FoundationsContainer _foundationsUI;

        public FoundationBinder(Foundations foundations, FoundationsContainer foundationsUI)
        {
            this._foundations = foundations;
            this._foundationsUI = foundationsUI;
        }

        public void Redraw()
        {
            _foundationsUI.Clear();
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
    }

    public class HomecellsBinder
    {
        Homecells _homecells;
        HomecellsContainer _homecellsUI;

        public HomecellsBinder(Homecells homecells, HomecellsContainer homecellsUI)
        {
            _homecells = homecells;
            _homecellsUI = homecellsUI;
        }

        public void Redraw()
        {
            _homecellsUI.Clear();
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

    public class TableauBinder
    {
        private Tableau _tableau;
        private TableauContainer _tableauUI;

        public TableauBinder(Tableau tableau, TableauContainer tableauUI) 
        {
            _tableau = tableau;
            _tableauUI = tableauUI;
        }

        public void Redraw()
        {
            _tableauUI.Clear();            
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
    }

    //public class Game
    //{
    //    /// <summary>
    //    /// 存放52張卡片
    //    /// </summary>
    //    List<CardView> Cards = new List<CardView>();


    //    /// <summary>
    //    /// 左上暫存區
    //    /// </summary>
    //    private Foundations tempZone;
    //    /// <summary>
    //    /// 右上完成區
    //    /// </summary>
    //    private Homecells completionZone;
    //    /// <summary>
    //    /// 下方待處理區
    //    /// </summary>
    //    private Tableau  waitZone;

    //    /// <summary>
    //    /// 目前遊戲正在進行中
    //    /// </summary>
    //    public bool IsPlaying { get; set; }


    //    /// <summary>
    //    /// 初始空畫面
    //    /// </summary>
    //    /// <param name="menuHeight"></param>
    //    public void InitScreen()
    //    {
    //        //空畫面            
    //        InitBoardScreen();
    //        //功能表單
    //        InitializeMenu();
    //    }
    //    /// <summary>
    //    /// 初始化遊戲事件
    //    /// </summary>
    //    public void InitEvents()
    //    {
    //        //game.Init(menuHeight);
    //        this.OnFinish += delegate ()
    //        {
    //            if (MessageBox.Show("你還要再一次嗎?", "新接龍", MessageBoxButtons.YesNo) == DialogResult.Yes)
    //            {
    //                this.Reset();
    //                this.Start();
    //            }
    //            else
    //            {
    //                this.form.Quit();
    //            }
    //        };

    //        this.OnFail += delegate ()
    //        {
    //            if (MessageBox.Show("你還要再一次嗎?", "新接龍", MessageBoxButtons.YesNo) == DialogResult.Yes)
    //            {
    //                this.Reset();
    //                this.Start();
    //            }
    //            else
    //            {
    //                this.form.Quit();
    //            }
    //        };
    //    }
    //    /// <summary>
    //    /// 初始化功能表單
    //    /// 暫未使用 但需保留
    //    /// </summary>
    //    private void InitializeMenu()
    //    {
    //        var self = this;

    //        var menu = new System.Windows.Forms.MenuStrip();

    //        menu.Dock = DockStyle.Top;
    //        menu.BackColor = Color.Silver;


    //        var menuItem0 = new ToolStripMenuItem();
    //        menuItem0.Text = "遊戲(&G)";
    //        menu.Items.Add(menuItem0);
    //        menuItem0.DropDownItems.Add("新遊戲", null).Click += delegate (object sender, EventArgs e)
    //        {
    //            if (this.IsPlaying &&
    //                MessageBox.Show("是否放棄這一局", "新接龍", MessageBoxButtons.YesNo) == DialogResult.No)
    //            {
    //                return;
    //            }
    //            this.Init();
    //        };
    //        menuItem0.DropDownItems.Add("選擇牌局", null);
    //        menuItem0.DropDownItems.Add("重啟牌局", null);
    //        menuItem0.DropDownItems.Add(new ToolStripSeparator());
    //        menuItem0.DropDownItems.Add("結束(&X)", null).Click += delegate (object sender, EventArgs e)
    //        {
    //            form.Quit();
    //        };

    //        var menuItem1 = new ToolStripMenuItem();
    //        menuItem1.Text = "說明(&H))";
    //        menu.Items.Add(menuItem1);
    //        menuItem1.DropDownItems.Add("關於新接龍", null);

    //        form.SetControl(menu);
    //    }        

    //    /// <summary>
    //    /// 遊戲初始
    //    ///     圖片載入
    //    ///     資料重置
    //    ///     事件掛入
    //    /// </summary>
    //    public void Init(int layoutMarginTop = 24)
    //    {                      
    //        //初始撲克牌圖檔與資料
    //        InitCards();

    //        //IsPlaying = true;

    //        int top;
    //        int left;

    //        left = 0;
    //        top = 0 + layoutMarginTop;
    //        tempZone = new Foundations(this.form);            
    //        tempZone.Init(cardWidth, cardHeight, left, top);
    //        tempZone.HolderClick += delegate (ColumnType zoneType, Slot slot)
    //        {
    //            this.TryAction(zoneType, slot);

    //        };

    //        left = this.boardWidth - (this.cardWidth * 4) - 9;
    //        top = 0 + layoutMarginTop;
    //        completionZone = new Homecells(this.form);
    //        completionZone.Init(cardWidth, cardHeight, left , top);
    //        completionZone.HolderClick += delegate (ColumnType zoneType, Slot slot)
    //        {
    //            this.TryAction(zoneType, slot);

    //        };

    //        int marginLeft = (boardWidth - cardWidth * 8) / 9;
    //        int marginTop = cardHeight / 6;
    //        left = marginLeft;
    //        top = cardHeight + 12 + layoutMarginTop;            
    //        waitZone = new Tableau (this.form);            
    //        waitZone.Init(cardWidth, cardHeight, left , top, marginLeft, marginTop);
    //        waitZone.HolderClick += delegate (ColumnType zoneType, Slot slot)
    //        {
    //            this.TryAction(zoneType, slot);
    //        };

    //        //將所有牌，初始放置在等待區
    //        for (int n = 0; n < this.Cards.Count; n++)
    //        {
    //            int locX = n % 8;
    //            CardView card = Cards[n];
    //            waitZone.SetCard(locX, card);
    //        }



    //    }

    //    /// <summary>
    //    /// 取得遊戲中，選取的撲克牌
    //    /// 回傳選取牌與所在的區域
    //    /// </summary>
    //    /// <returns></returns>
    //    [Obsolete]
    //    public CardLocation GetSelectedCardInfo()
    //    {
    //        CardLocation result;
    //        result = this.waitZone.GetSelectedInfo();
    //        if (result != null)
    //        {
    //            return result;
    //        }
    //        result = this.tempZone.GetSelectedInfo();
    //        if (result != null)
    //        {
    //            return result;
    //        }
    //        return null;
    //    }

    //    /// <summary>
    //    /// 試著選取、取消選取、搬移
    //    /// </summary>
    //    public void TryAction(ColumnType zoneType, Slot slot)
    //    {
    //        var selectedCard = this.GetActivedCard();
    //        if (slot.Zone is Tableau )
    //        {
    //            string message;
    //            CardMoveAction moveResult = waitZone.TryAction(slot.Index, out message);

    //        }
    //        else if (slot.Zone is Homecells)
    //        {
    //            if (selectedCard != null)
    //            {
    //                if (SlotUtil.Move(selectedCard.Slot, slot) == false)
    //                {
    //                    this.ShowGameAlert();
    //                    //selectedCard = this.GetActivedCard();
    //                }
    //            }

    //            //if (slot.IsFull)
    //            //{
    //            //    //nothing happend
    //            //}
    //            //else if (selectedCard == null)
    //            //{
    //            //    //nothing happend
    //            //}
    //            //else
    //            //{
    //            //    if (completionZone.MoveCard(slot.Index, selectedCard) == false)
    //            //    {
    //            //        this.ShowGameAlert();
    //            //    }
    //            //}
    //        }
    //        else if (slot.Zone is Foundations)
    //        {
    //            //CardMoveAction moveResult = tempZone.TryAction(slot.Index, out message);
    //            if (selectedCard != null && selectedCard.Slot == slot)
    //            {
    //                slot.LastCard().Actived = false;
    //                //this.tempZone.DeselectSlots();
    //            }
    //            else if (selectedCard == null && slot.IsFull)
    //            {
    //                slot.LastCard().Actived = true;
    //            }
    //            else if (selectedCard != null && slot.IsFull)
    //            {
    //                ShowGameAlert();
    //            }

    //            if (selectedCard != null && slot.IsFull == false)
    //            {
    //                Console.WriteLine(string.Format("移到暫存區第 {0} 排，目前選取 {1}", slot.Index, selectedCard.ToString()));
    //                //selectedCard.Slot.RemoveCard(selectedCard);
    //                //slot.AddCard(selectedCard);
    //                selectedCard.Actived = false;
    //                tempZone.MoveCard(slot.Index, selectedCard);
    //            }
    //        }
    //    }

    //    private void ShowGameAlert()
    //    {
    //        SystemSounds.Asterisk.Play();
    //        MessageBox.Show("此步犯規");
    //        var selectedCard = GetActivedCard();
    //        if (selectedCard != null)
    //        {
    //            selectedCard.Actived = false;
    //        }
    //    }

    //    public CardView GetActivedCard()
    //    {
    //        foreach(var card in this.Cards)
    //        {
    //            if (card.Actived)
    //            {
    //                return card;
    //            }
    //        }
    //        return null;
    //    }

    //    /// <summary>
    //    /// 重置排局
    //    /// 重置畫面
    //    /// 
    //    /// 未實作
    //    /// </summary>
    //    public void Reset()
    //    {

    //    }

    //    private void InitCards()
    //    {
    //        Deck deck = Deck.Create().Shuffle(3);            
    //        for (int i = 0; i < 52; i++) {
    //            CardView card = new CardView(
    //                this,
    //                deck.Draw(), this.cardWidth, this.cardHeight, this.form);
    //            Cards.Add(card);
    //        }                    
    //    }

    //    public void FinishGame()
    //    {

    //    }

    //    public void AutoPutCardToCompeleteZone()
    //    {

    //    }

    //    public void Start()
    //    {


    //    }


    //    public void MoveToCompletionZone(CardView card, int x)
    //    {
    //        this.completionZone.SetCard(x, card);
    //    }


    //    private IGameForm form;
    //    public Game(IGameForm form)
    //    {
    //        this.form = form;
    //    }

    //    private int cardWidth { get; set; }
    //    private int cardHeight { get; set; }

    //    private int boardWidth { get; set; }
    //    private int boardHeight { get; set; }
    //    public event GameEventHandler OnFinish;
    //    public event GameEventHandler OnFail;

    //    private void InitBoardScreen()
    //    {
    //        boardWidth = 800;
    //        boardHeight = 600;


    //        form.Width = boardWidth;
    //        form.Height = boardHeight;
    //        form.BackColor = Color.FromArgb(0, 123, 0);


    //        cardWidth = (int)(Math.Floor((decimal)boardWidth / 9));
    //        cardHeight = (int)(cardWidth * 1.38);
    //    }


    //}



}
