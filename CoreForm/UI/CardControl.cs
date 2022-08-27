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
        
        public Image ActiveImage { get; private set; }

        public CardControl(int index, int cardWidth, int cardHeight, Card card)
        {
            _card = card;
            _index = index;
            _cardWidth = cardWidth;
            _cardHeight = cardHeight;

            this.BorderStyle = BorderStyle.None;
            this.BackColor = Color.Black;
            this.Margin = new Padding(0);
            InitImage();
            ResizeTo();
        }

        private void ResizeTo()
        {
            this.Location = new Point(0, _index * (int)(_cardHeight / 6.1f));
            this.Width = _cardWidth;
            this.Height = _cardHeight;
            this.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void InitImage()
        {
            var assembly = System.Reflection.Assembly.GetEntryAssembly();            
            Stream resource = assembly
                .GetManifestResourceStream("FreeCellSolitaire.assets.img." + GetImageFileName() + ".png");
            Image img = Image.FromStream(resource);
            this.Image = img;
            this.ActiveImage = img.DrawAsNegative();
        }

        private string GetImageFileName()
        {
            return _card.Suit.ToString() + "-" + _card.Number.ToString("00");
        }
    }
}
