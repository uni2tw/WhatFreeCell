using CoreForm.Utilities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CoreForm.UI
{
    public interface IGame2Form
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public Color BackColor { get; set; }
        
        void SetControlReady(Control control);
    }

    public delegate void GameEventHandler();

    public class Game2
    {
        List<CardView2> Cards = new List<CardView2>();
        TempZone2 tempZone;
        CompletionZone2 completionZone;
        WaitingZone2 waitZone;
        //52張牌        
        public void Init()
        {            
            InitBoard();
            InitCards();

            tempZone = new TempZone2(this.form);
            tempZone.Init(cardWidth, cardHeight, 0, 0);
            tempZone.HolderClick += delegate (GameZoneType zoneType, Slot2 slot)
            {
                MessageBox.Show(zoneType.ToString() + " was click" + slot.Index);
            };

            completionZone = new CompletionZone2(this.form);
            completionZone.Init(cardWidth, cardHeight, this.boardWidth - (this.cardWidth * 4) - 9 , 0);
            completionZone.HolderClick += delegate (GameZoneType zoneType, Slot2 slot)
            {
                MessageBox.Show(zoneType.ToString() + " was click" + slot.Index);
            };

            int marginLeft = (boardWidth - cardWidth * 8) / 9;
            int top = cardHeight + 12;
            int marginTop = cardHeight / 6;
            int left = marginLeft;

            waitZone = new WaitingZone2(this.form);
            waitZone.Init(cardWidth, cardHeight, left , top, marginLeft, marginTop);
            waitZone.HolderClick += delegate (GameZoneType zoneType, Slot2 slot)
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
                CardView2 card = new CardView2(
                    deck.Draw(), this.cardWidth, this.cardHeight, this.form);
                Cards.Add(card);
            }                    
        }

        public void Start()
        {
            for (int x = 0; x < 4; x++)
            {
                CardView2 card = Cards[x];
                if (tempZone.IsAvailableFor(x, card))
                {
                    tempZone.SetCard(x, card);
                }
            }


            for (int n = 4; n < 12; n++)
            {
                int x = n % 4;
                CardView2 card = Cards[n];                
                if (completionZone.IsAvailableFor(x, card))
                {
                    completionZone.SetCard(x, card);
                }
            }

            for (int n = 12; n < 52; n++)
            {
                int x = n % 8;
                CardView2 card = Cards[n];
                if (waitZone.IsAvailableFor(x, card))
                {
                    waitZone.SetCard(x, card);
                }
            }

        }
        
        private IGame2Form form;
        public Game2(IGame2Form form)
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
    public class CardView2
    {
        public CardView2(Card card, int cardWidth, int cardHeight, IGame2Form form)
        {
            this.Data = card;

            PictureBox viewControl = new PictureBox
            {
                Location = new Point(0, 0),
                Width = cardWidth,
                Height = cardHeight,
                SizeMode = PictureBoxSizeMode.StretchImage,
                Image = null
            };            
            InitImage();
            this.View = viewControl;
            this.View.Image = this.Image;
            form.SetControlReady(this.View);
        }
        public GameZoneType ZoneType { get; set; }
        public bool actived;
        public bool Actived
        {
            get
            {
                return actived;
            }
            set
            {
                if (this.actived != value)
                {
                    this.actived = value;
                    if (this.actived)
                    {
                        this.View.Image = this.ActivedImage;
                    }
                    else
                    {
                        this.View.Image = this.Image;
                    }
                }
            }
        }
        public CardSuit Suit
        {
            get
            {
                return this.Data.Suit;
            }
        }
        public int Number
        {
            get
            {
                return this.Data.Number;
            }
        }
        private string GetImageFileName()
        {
            return string.Format("{0}-{1}.png",this.Data.Suit, this.Data.Number.ToString("00"));
        }

        private void InitImage()
        {
            var assembly = System.Reflection.Assembly.GetEntryAssembly();
            Stream resource = assembly
                .GetManifestResourceStream("CoreForm.assets.img." + GetImageFileName());
            Image img = Image.FromStream(resource);
            this.Image = img;
            this.ActivedImage = img.DrawAsNegative();
        }
        private Image Image { get; set; }
        private Image ActivedImage { get; set; }
        private Card Data { get; set; }
        public PictureBox View { get; private set; }
    }
    /// <summary>
    /// 一排
    /// </summary>
    public class Slot2
    {
        public int right { get; private set; }
        public int top { get; private set; }
        public int cardHeight { get; private set; }

        public int Index { get; set; }
        /// <summary>
        /// 一列最多可以放幾張牌
        /// </summary>
        public int Capacity { get; set; }

        public int Count { 
            get
            {
                return Cards.Count;
            } 
        }

        public Slot2(int right, int top, int cardHeight, Control holder, int capacity, int index)
        {
            this.right = right;
            this.top = top;
            this.cardHeight = cardHeight;
            this.Holder = holder;
            this.Capacity = capacity;
            this.Index = index;
            Cards = new List<CardView2>();
        }

        public Point GetLocation(int y)
        {
            return new Point(right, top + y * (cardHeight / 6));
        }

        private List<CardView2> Cards { get; set; }
        /// <summary>
        /// 支架
        /// </summary>
        public Control Holder { get; set; }
        public bool IsFull
        {
            get
            {
                return this.Cards.Count >= this.Capacity;
            }
        }

        public bool AddCard(CardView2 card)
        {
            if (Cards == null)
            {
                Cards = new List<CardView2>();
            }
            if (this.IsFull == false)
            {
                this.Cards.Add(card);
                return true;
            } 
            else
            {
                return false;
            }
        }

        public CardView2 LastCard()
        {
            return Cards.LastOrDefault();
        }

        internal Point GetLocation(object count)
        {
            throw new NotImplementedException();
        }
    }



    public interface IZone2
    {
        bool CanSwap { get; }
        bool CanMoveIn { get; }
        bool CanMoveOut { get; }
        int Capacity { get; }

        List<Slot2> Slots { get; set; }

        bool IsAvailableFor(int x, CardView2 card);        

        void SetCard(int x, CardView2 card);
    }

    public class WaitingZone2 : IZone2
    {
        private IGame2Form form;

        public WaitingZone2(IGame2Form form)
        {
            this.form = form;
            this.Slots = new List<Slot2>();
        }

        public bool CanSwap
        {
            get
            {
                return true;
            }
        }
        public bool CanMoveIn
        {
            get
            {
                return true;
            }
        }
        public bool CanMoveOut
        {
            get
            {
                return true;
            }
        }
        public int Capacity
        {
            get
            {
                return 13;
            }
        }

        public List<Slot2> Slots { get; set; }
        public event ZoneHolderHandler HolderClick;

        public void Init(int cardWidth, int cardHeight, int left, int top, int marginLeft, int marginTop)
        {
            for (int i = 0; i < 8; i++)
            {
                Control holder = new Panel
                {
                    Location = new Point(left, top),
                    Width = cardWidth,
                    Height = cardHeight,
                    BorderStyle = BorderStyle.FixedSingle,
                    BackgroundImageLayout = ImageLayout.Stretch
                };

                form.SetControlReady(holder);
                var slot = new Slot2(left, top, cardHeight, holder, this.Capacity, i);
                Slots.Add(slot);
                holder.Click += delegate (object sender, EventArgs e)
                {
                    HolderClick?.Invoke(GameZoneType.Waiting, slot);
                };
                left = left + cardWidth + marginLeft; 
            }
        }

        public bool IsAvailableFor(int x, CardView2 card)
        {
            return this.Slots[x].IsFull == false;
        }

        public void SetCard(int x, CardView2 card)
        {
            if (this.Slots[x].IsFull)
            {
                throw new Exception("出錯了");
            }

            Slot2 slot = this.Slots[x];

            card.View.Visible = true;
            card.View.Location = this.Slots[x].GetLocation(slot.Count);
            card.View.BringToFront();
            card.ZoneType = GameZoneType.Waiting;
            slot.AddCard(card);
        }
    }
    public class TempZone2 : IZone2
    {
        IGame2Form form;
        public event ZoneHolderHandler HolderClick;
        public TempZone2(IGame2Form form)
        {
            this.form = form;
            this.Slots = new List<Slot2>();
        }
        public bool CanSwap
        {
            get
            {
                return true;
            }
        }
        public bool CanMoveIn
        {
            get
            {
                return true;
            }
        }
        public bool CanMoveOut
        {
            get
            {
                return true;
            }
        }
        public int Capacity
        {
            get
            {
                return 1;
            }
        }

        public List<Slot2> Slots { get; set; }
        public void Init(int cardWidth, int cardHeight, int right, int top)
        {
            for (int i = 0; i < 4; i++)
            {                
                Control holder = new Panel
                {
                    Location = new Point(right, top),
                    Width = cardWidth,
                    Height = cardHeight,
                    BorderStyle = BorderStyle.FixedSingle,
                    BackgroundImageLayout = ImageLayout.Stretch
                };

                form.SetControlReady(holder);
                var slot = new Slot2(right, top, cardHeight, holder, Capacity, i);                
                Slots.Add(slot);
                holder.Click += delegate (object sender, EventArgs e)
                {
                    HolderClick?.Invoke(GameZoneType.Temp, slot);
                };
                right = right + cardWidth;
            }
        }

        public void SetCard(int x, CardView2 card)
        {
            //if (this.Slots[x].Cards.Count >= this.QueueLimit)
            if (this.Slots[x].IsFull)
            {
                throw new Exception("出錯了");
            }
            
            this.Slots[x].AddCard(card);
            this.Slots[x].AddCard(card);
            card.View.Visible = true;
            card.View.Location = this.Slots[x].GetLocation(0);
            card.View.BringToFront();
            card.ZoneType = GameZoneType.Temp;
            
        }

        public bool IsAvailableFor(int x, CardView2 card)
        {            
            return this.Slots[x].IsFull == false;
        }
    }

    public delegate void ZoneHolderHandler(GameZoneType zoneType, Slot2 slot);    

    public class CompletionZone2 : IZone2
    {
        private IGame2Form form;
        public event ZoneHolderHandler HolderClick;

        public CompletionZone2(IGame2Form form)
        {
            this.form = form;
            this.Slots = new List<Slot2>();
        }

        public bool CanSwap
        {
            get
            {
                return false;
            }
        }
        public bool CanMoveIn
        {
            get
            {
                return true;
            }
        }
        public bool CanMoveOut
        {
            get
            {
                return false;
            }
        }
        public int Capacity
        {
            get
            {
                return 13;
            }
        }

        public List<Slot2> Slots { get; set; }        

        public void Init(int cardWidth, int cardHeight, int left, int top)
        {
            for (int i = 0; i < 4; i++)
            {
                Control holder = new Panel
                {
                    Location = new Point(left, top),
                    Width = cardWidth,
                    Height = cardHeight,
                    BorderStyle = BorderStyle.FixedSingle,
                    BackgroundImageLayout = ImageLayout.Stretch
                };
                form.SetControlReady(holder);
                var slot = new Slot2(left, top, cardHeight, holder, Capacity, i);
                Slots.Add(slot);

                holder.Click += delegate (object sender, EventArgs e)
                {
                    HolderClick?.Invoke(GameZoneType.Completion, slot);
                };

                left = left + cardWidth;
            }
        }

        public bool IsAvailableFor(int x, CardView2 card)
        {
            if (this.Slots[x].IsFull)
            {
                return false;
            }
            CardView2 lastCard = this.Slots[x].LastCard();
            if (lastCard == null && card.Number == 1)
            {
                return true;
            }
            if (lastCard != null && lastCard.Suit == card.Suit && card.Number - lastCard.Number == 1)
            {
                return true;
            }
            return false;
        }

        public void SetCard(int x, CardView2 card)
        {
            if (this.Slots[x].IsFull)
            {
                throw new Exception("出錯了");
            }

            Slot2 slot = this.Slots[x];

            this.Slots[x].AddCard(card);
            card.View.Visible = true;
            card.View.Location = this.Slots[x].GetLocation(0);
            card.View.BringToFront();
            card.ZoneType = GameZoneType.Completion;
        }
    }

}
