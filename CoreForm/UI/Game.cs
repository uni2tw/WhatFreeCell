using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
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
        List<CardView> Cards = new List<CardView>();
        private TempZone tempZone;
        private CompletionZone completionZone;
        private WaitingZone waitZone;
        CardView selectedCard;
        //52張牌        
        public void Init()
        {            
            InitBoard();
            InitCards();

            tempZone = new TempZone(this.form);
            tempZone.Init(cardWidth, cardHeight, 0, 0);
            tempZone.HolderClick += delegate (GameZoneType zoneType, Slot slot)
            {
                MessageBox.Show(zoneType.ToString() + " was click" + slot.Index);
            };

            completionZone = new CompletionZone(this.form);
            completionZone.Init(cardWidth, cardHeight, this.boardWidth - (this.cardWidth * 4) - 9 , 0);
            completionZone.HolderClick += delegate (GameZoneType zoneType, Slot slot)
            {
                MessageBox.Show(zoneType.ToString() + " was click" + slot.Index);
            };

            int marginLeft = (boardWidth - cardWidth * 8) / 9;
            int top = cardHeight + 12;
            int marginTop = cardHeight / 6;
            int left = marginLeft;

            waitZone = new WaitingZone(this.form);
            waitZone.Init(cardWidth, cardHeight, left , top, marginLeft, marginTop);
            waitZone.HolderClick += delegate (GameZoneType zoneType, Slot slot)
            {
                MessageBox.Show(zoneType.ToString() + " click slot-" + slot.Index);
            };
        }
        /// <summary>
        /// 重置排局
        /// 重置畫面
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
            //for (int x = 0; x < 4; x++)
            //{
            //    CardView2 card = Cards[x];
            //    if (tempZone.IsAvailableFor(x, card))
            //    {
            //        tempZone.SetCard(x, card);
            //    }
            //}


            //for (int n = 4; n < 12; n++)
            //{
            //    int x = n % 4;
            //    CardView2 card = Cards[n];                
            //    if (completionZone.IsAvailableFor(x, card))
            //    {
            //        completionZone.SetCard(x, card);
            //    }
            //}

            for (int n = 0; n < 52; n++)
            {
                int x = n % 8;
                CardView card = Cards[n];
                //if (waitZone.IsAvailableFor(x, card))
                //{                
                //waitZone.SetCard(x, card);
                MoveTo(card, GameZoneType.Waiting, x);
                //}
            }

            
            selectedCard = waitZone.SelectCard(5);
            if (selectedCard != null)
            {
                MoveTo(selectedCard as CardView, GameZoneType.Temp, 0);
            }
            //selectedCard = waitZone.SelectCard(5);
        }

        public void MoveTo(CardView card,GameZoneType target, int x)
        {
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
            } else if (target == GameZoneType.Temp)
            {
                this.tempZone.SetCard(x, card);
            }
            else if (target == GameZoneType.Completion)
            {
                this.completionZone.SetCard(x, card);
            }
            
            card.Actived = false;            
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

        private void InitBoard()
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
