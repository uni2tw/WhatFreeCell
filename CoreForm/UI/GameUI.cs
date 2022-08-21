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
        void InitScreen();
        void Reset();
        void Start();
        void InitEvents();
        bool IsPlaying { get; }
    }


    public class GameUI : IGameUI
    {
        IGameForm _form;
        IGame _game;
        private TableauContainer _tableauUI;
        private HomecellsContainer _homecellsUI;
        private FoundationsContainer _foundationsUI;

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
        public void InitScreen()
        {
            //空畫面            
            InitBoardScreen();
            //功能表單
            InitializeMenu();

            InitControls();
        }



        private int boardWidth { get; set; }
        private int boardHeight { get; set; }
        private int cardWidth { get; set; }
        private int cardHeight { get; set; }
        private void InitBoardScreen()
        {
            boardWidth = 800;
            boardHeight = 600;

            _form.Width = boardWidth;
            _form.Height = boardHeight;
            _form.BackColor = Color.FromArgb(0, 123, 0);


            cardWidth = (int)(Math.Floor((decimal)boardWidth / 9));
            cardHeight = (int)(cardWidth * 1.38);
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
            menuItem0.DropDownItems.Add("選擇牌局", null);
            menuItem0.DropDownItems.Add("重啟牌局", null);
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
            int layoutMarginTop = 24;            
            int left = 0;            
            int top = layoutMarginTop;
            this._foundationsUI = new FoundationsContainer(this._form, columnNumber: 4, left, top, cardWidth, cardHeight);

            this._form.SetControlReady(this._foundationsUI);
            

            //        waitZone.Init(cardWidth, cardHeight, left , top, marginLeft, marginTop);
            //        waitZone.HolderClick += delegate (ColumnType zoneType, Slot slot)


            //this._tableauUI = new TableauContainer(this._form);
            //this._homecellsUI = new HomecellsContainer(this._form);

        }

        public void Reset()
        {
            _game = new Game();
            var tableau = new Tableau(_game);
            var homecells = new Homecells(_game);
            var foundations = new Foundations(_game);
            var deck = Deck.Create().Shuffle(101);            
            tableau.Init(deck); 
        }

        public void Start()
        {
        
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
