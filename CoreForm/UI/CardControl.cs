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
        public Card Card { get; set; }
        
        public Image ActiveImage { get; private set; }

        public CardControl(int cardWidth, int cardHeight)
        {
            PictureBox viewControl = new PictureBox
            {
                Location = new Point(0, 0),
                Width = cardWidth,
                Height = cardHeight,
                SizeMode = PictureBoxSizeMode.Normal,
                Image = null
            };
            InitImage();
        }

        private void InitImage()
        {
            var assembly = System.Reflection.Assembly.GetEntryAssembly();
            Stream resource = assembly
                .GetManifestResourceStream("FreeCellSolitaire.assets.img." + GetImageFileName());
            Image img = Image.FromStream(resource);
            this.Image = img;
            this.ActiveImage = img.DrawAsNegative();
        }

        private string GetImageFileName()
        {
            return Card.Suit.ToString() + "-" + Card.Number.ToString("00");
        }
    }
}
