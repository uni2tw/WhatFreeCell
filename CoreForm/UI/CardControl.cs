using CoreForm.UI;
using CoreForm.Utilities;
using FreeCellSolitaire.Core.CardModels;
using FreeCellSolitaire.Core.GameModels;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FreeCellSolitaire.UI
{
    public class CardControl : PictureBox
    {
        Card _card;
        int _index;
        int _cardWidth;
        int _cardHeight;
        public GeneralColumnPanel Owner { get; private set; }
        public Image ActiveImage { get; private set; }
        public Image InactivedImage { get; private set; }

        public Card Card { get
            {
                return _card;
            } 
        }

        public void SetIndex(int index)
        {
            _index = index;
        }

        public CardControl(GeneralColumnPanel owner, int cardWidth, int cardHeight, Card card)
        {
            _card = card;
            Owner = owner;
            
            this.BorderStyle = BorderStyle.None;
            this.BackColor = Color.Black;
            this.Margin = new Padding(0);

            ResizeTo(cardWidth, cardHeight);
            InitImage();            
        }

        public void ResizeTo(int cardWidth, int cardHeight)
        {            
            this.Width = cardWidth;
            this.Height = cardHeight;
        }

        public bool IsAssignedCard(Card card)
        {
            return _card.Equals(card);
        }

        public void Redraw(int cardTop)
        {
            this.Location = new Point(0, cardTop);
            this.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void InitImage()
        {
            var assembly = System.Reflection.Assembly.GetEntryAssembly();            
            Stream resource = assembly
                .GetManifestResourceStream("FreeCellSolitaire.assets.img." + GetImageFileName() + ".png");
            Image img = Image.FromStream(resource);
            this.Image = img;
            this.InactivedImage = img;
            if (_card.IsBlack())
            {
                this.ActiveImage = img.DrawAsBlueLight();
            }
            else
            {
                this.ActiveImage = img.DrawAsNegative();
            }            
            //this.Image = ActiveImage;
        }

        private string GetImageFileName()
        {
            return _card.Suit.ToString() + "-" + _card.Number.ToString("00");
        }

        public void SetActived(bool isActivated)
        {
            if (isActivated == false && this.Image != this.InactivedImage)
            {
                this.Image = this.InactivedImage;
            }
            if (isActivated && this.Image != this.ActiveImage)
            {
                this.Image = this.ActiveImage;
            }
        }
    }
}
