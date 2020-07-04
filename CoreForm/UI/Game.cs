using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Windows.Forms;

namespace CoreForm.UI
{
    public interface IGameForm
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public Color BackColor { get; set; }
        
        void SetControlReady(Control control);
    }

    public delegate void GameEventHandler();


    public class Game
    {
        /// <summary>
        /// 存放52張卡片
        /// </summary>
        List<CardView> Cards = new List<CardView>();
        /// <summary>
        /// 左上暫存區
        /// </summary>
        private TempZone tempZone;
        /// <summary>
        /// 右上完成區
        /// </summary>
        private CompletionZone completionZone;
        /// <summary>
        /// 下方待處理區
        /// </summary>
        private WaitingZone waitZone;

        /// <summary>
        /// 遊戲初始
        ///     圖片載入
        ///     資料重置
        ///     事件掛入
        /// </summary>
        public void Init(int layoutMarginTop = 0)
        {          
            //初始遊戲基本畫面
            InitBoardScreen();
            //初始撲克牌圖檔與資料
            InitCards();



            int top;
            int left;

            left = 0;
            top = 0 + layoutMarginTop;
            tempZone = new TempZone(this.form);            
            tempZone.Init(cardWidth, cardHeight, left, top);
            tempZone.HolderClick += delegate (GameZoneType zoneType, Slot slot)
            {
                this.TryAction(zoneType, slot);
                //var cardInfo = this.GetSelectedCardInfo();
                //string message = string.Format("{0} was selected, click {1}-{2}",
                //    cardInfo,
                //    zoneType, slot.Index);
                //MessageBox.Show(message);
            };

            left = this.boardWidth - (this.cardWidth * 4) - 9;
            top = 0 + layoutMarginTop;
            completionZone = new CompletionZone(this.form);
            completionZone.Init(cardWidth, cardHeight, left , top);
            completionZone.HolderClick += delegate (GameZoneType zoneType, Slot slot)
            {
                this.TryAction(zoneType, slot);
                //var cardInfo = this.GetSelectedCardInfo();
                //if (cardInfo != null)
                //{
                //    string message = string.Format(
                //        "{0} was selected, click {1}", cardInfo, zoneType, slot.Index);
                //    MessageBox.Show(message);
                //}                
            };

            int marginLeft = (boardWidth - cardWidth * 8) / 9;
            int marginTop = cardHeight / 6;
            left = marginLeft;
            top = cardHeight + 12 + layoutMarginTop;            
            waitZone = new WaitingZone(this.form);            
            waitZone.Init(cardWidth, cardHeight, left , top, marginLeft, marginTop);
            waitZone.HolderClick += delegate (GameZoneType zoneType, Slot slot)
            {
                this.TryAction(zoneType, slot);
                //string message;
                //CardMoveAction moveResult = waitZone.TryAction(slot.Index, out message);
                //if (moveResult == CardMoveAction.Move)
                //{
                //    this.OnMove(zoneType);
                //}
            };

            //將所有牌，初始放置在等待區
            for (int n = 0; n < this.Cards.Count; n++)
            {
                int locX = n % 8;
                CardView card = Cards[n];
                waitZone.SetCard(locX, card);
            }



        }

        /// <summary>
        /// 取得遊戲中，選取的撲克牌
        /// 回傳選取牌與所在的區域
        /// </summary>
        /// <returns></returns>
        public CardLocation GetSelectedCardInfo()
        {
            CardLocation result;
            result = this.waitZone.GetSelectedInfo();
            if (result != null)
            {
                return result;
            }
            result = this.tempZone.GetSelectedInfo();
            if (result != null)
            {
                return result;
            }
            return null;
        }

        /// <summary>
        /// 試著選取、取消選取、搬移
        /// </summary>
        public void TryAction(GameZoneType zoneType, Slot slot)
        {
            if (zoneType == GameZoneType.Waiting)
            {
                string message;
                CardMoveAction moveResult = waitZone.TryAction(slot.Index, out message);







            }
            else if (zoneType == GameZoneType.Completion)
            {
                MessageBox.Show(string.Format("new action(c) {0}-{1}", zoneType, slot.Index));
            }
            else if (zoneType == GameZoneType.Temp)
            {
                string message;
                CardMoveAction moveResult = tempZone.TryAction(slot.Index, out message);

                var card = GetActiviedCard();
                if (card != null)
                {                    
                    MessageBox.Show(string.Format("移到暫存區第 {0} 排，目前選取 {1}", slot.Index, card.ToString()));
                }
                else
                {
                    MessageBox.Show(string.Format("移到暫存區第 {0} 排，目前無選取", slot.Index));
                }
            }
        }

        public CardView GetActiviedCard()
        {
            foreach(var card in this.Cards)
            {
                if (card.Actived)
                {
                    return card;
                }
            }
            return null;
        }

        /// <summary>
        /// 重置排局
        /// 重置畫面
        /// 
        /// 未實作
        /// </summary>
        public void Reset()
        {
            
        }

        private void InitCards()
        {
            Deck deck = Deck.Create().Shuffle(1);            
            for (int i = 0; i < 52; i++) {
                CardView card = new CardView(
                    this,
                    deck.Draw(), this.cardWidth, this.cardHeight, this.form);
                Cards.Add(card);
            }                    
        }

        public void FinishGame()
        {

        }

        public void AutoPutCardToCompeleteZone()
        {

        }

        public void Start()
        {
  
           
        }


        public void MoveToCompletionZone(CardView card, int x)
        {
            this.completionZone.SetCard(x, card);
        }


        private IGameForm form;
        public Game(IGameForm form)
        {
            this.form = form;
        }

        private int cardWidth { get; set; }
        private int cardHeight { get; set; }

        private int boardWidth { get; set; }
        private int boardHeight { get; set; }
        public event GameEventHandler OnFinish;
        public event GameEventHandler OnFail;

        private void InitBoardScreen()
        {
            boardWidth = 800;
            boardHeight = 600;


            form.Width = boardWidth;
            form.Height = boardHeight;
            form.BackColor = Color.FromArgb(0, 123, 0);


            cardWidth = (int)(Math.Floor((decimal)boardWidth / 9));
            cardHeight = (int)(cardWidth * 1.38);
        }


    }



}
