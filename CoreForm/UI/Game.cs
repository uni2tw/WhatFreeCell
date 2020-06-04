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
        /// 目前選取的卡片
        /// </summary>
        private CardView selectedCard;
        /// <summary>
        /// 遊戲初始
        ///     圖片載入
        ///     資料重置
        ///     事件掛入
        /// </summary>
        public void Init()
        {          
            //初始遊戲基本畫面
            InitBoardScreen();
            //初始撲克牌圖檔與資料
            InitCards();

            int top;
            int left;

            left = 0;
            top = 0;            
            tempZone = new TempZone(this.form);            
            tempZone.Init(cardWidth, cardHeight, left, top);
            tempZone.HolderClick += delegate (GameZoneType zoneType, Slot slot)
            {
                MessageBox.Show(zoneType.ToString() + " was click" + slot.Index);
            };

            left = this.boardWidth - (this.cardWidth * 4) - 9;
            top = 0;            
            completionZone = new CompletionZone(this.form);
            completionZone.Init(cardWidth, cardHeight, left , top);
            completionZone.HolderClick += delegate (GameZoneType zoneType, Slot slot)
            {
                MessageBox.Show(zoneType.ToString() + " was click" + slot.Index);
            };

            int marginLeft = (boardWidth - cardWidth * 8) / 9;
            int marginTop = cardHeight / 6;
            left = marginLeft;
            top = cardHeight + 12;            
            waitZone = new WaitingZone(this.form);            
            waitZone.Init(cardWidth, cardHeight, left , top, marginLeft, marginTop);
            waitZone.HolderClick += delegate (GameZoneType zoneType, Slot slot)
            {
                if (zoneType == GameZoneType.Waiting)
                {
                    string message;
                    if (waitZone.TrySelect(slot.Index, out message) == false)
                    {
                        if (string.IsNullOrEmpty(message) == false)
                        {
                            SystemSounds.Asterisk.Play();
                            MessageBox.Show(message);                                                        
                        }
                        waitZone.DeselectSlots();
                    }
                }
                else
                {
                    MessageBox.Show(zoneType.ToString() + " click slot-" + slot.Index);
                }
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

        public void Start()
        {
  
            return;
            for (int n = 0; n < 52; n++)
            {
                int locX = n % 8;
                CardView card = Cards[n];
                //if (waitZone.IsAvailableFor(x, card))
                //{                
                //waitZone.SetCard(x, card);
                MoveTo(card, GameZoneType.Waiting, locX, true);
                //}
            }

            
            selectedCard = waitZone.SelectLastCard(5);
            if (selectedCard != null)
            {
                if (MoveTo(selectedCard, GameZoneType.Temp, 0, false) == false)
                {
                    MessageBox.Show("移動失敗");
                } else
                {
                    selectedCard = null;
                }
            }
            selectedCard = waitZone.SelectLastCard(6);
            if (selectedCard != null)
            {
                if (MoveTo(selectedCard, GameZoneType.Temp, 0, false) == false)
                {
                    MessageBox.Show("移動失敗");
                }
                else
                {
                    selectedCard = null;
                }
            }

            //selectedCard = waitZone.SelectLastCard(5);
            //MoveTo(selectedCard as CardView, GameZoneType.Temp, 0, false);
        }


        public bool MoveTo(CardView card,GameZoneType target, int x, bool force)
        {
            if (force == false)
            {
                if (target == GameZoneType.Waiting && this.waitZone.IsAvailableFor(x, card) == false)
                {
                    return false;
                }
                else if (target == GameZoneType.Temp && this.tempZone.IsAvailableFor(x, card) == false)
                {
                    return false;
                }
                else if (target == GameZoneType.Completion && this.completionZone.IsAvailableFor(x, card) == false)
                {
                    return false;
                }
            }

            if (card.ZoneType == GameZoneType.Waiting)
            {                
                this.waitZone.RemoveCard(selectedCard);
            }
            else if (card.ZoneType == GameZoneType.Temp)
            {
                this.tempZone.RemoveCard(selectedCard);
            }
            else if (card.ZoneType == GameZoneType.Completion)
            {
                this.completionZone.RemoveCard(selectedCard);
            }

            if (target == GameZoneType.Waiting)
            {
                this.waitZone.SetCard(x, card);
            } 
            else if (target == GameZoneType.Temp)
            {
                this.tempZone.SetCard(x, card);
            }
            else if (target == GameZoneType.Completion)
            {
                this.completionZone.SetCard(x, card);
            }
            
            card.Actived = false;

            return true;
        }
        public void MoveToCompletionZone(CardView card, int x)
        {
            this.completionZone.SetCard(x, card);
        }

        private void UnSelectCard()
        {
            if (selectedCard == null)
            {
                return;
            }
            if (selectedCard.Actived)
            {
                selectedCard.Actived = false;
            }
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
