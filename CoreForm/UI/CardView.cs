using CoreForm.FreeCell;
using CoreForm.Utilities;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace CoreForm.UI
{
    public class CardView : ICardBase
    {
        private Game game;
        public CardView(Game game, Card card, int cardWidth, int cardHeight, IGameForm form)
        {
            this.game = game;
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
        private bool actived;
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



}
