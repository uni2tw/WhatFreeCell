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
        CardView _cardView;
        IGameUI _gameUI;
        int _index;
        int _cardWidth;
        int _cardHeight;
        public GeneralColumnPanel Owner { get; private set; }
        public Image ActiveImage { get; private set; }
        public Image InactivedImage { get; private set; }

        public CardView CardView
        {
            get
            {
                return _cardView;
            }
        }

        public void SetIndex(int index)
        {
            _index = index;
        }

        public CardControl(GeneralColumnPanel owner, int cardWidth, int cardHeight, CardView card, IGameUI gameUI)
        {
            _cardView = card;
            _gameUI = gameUI;
            Owner = owner;

            this.BorderStyle = BorderStyle.None;
            this.BackColor = Color.Black;
            this.Margin = new Padding(0);
            this.SizeMode = PictureBoxSizeMode.StretchImage;

            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);

            this.MouseEnter += CardControl_MouseEnter;
            this.MouseLeave += CardControl_MouseLeave;

            ResizeTo(cardWidth, cardHeight);
            InitImage();
        }

        private void CardControl_MouseEnter(object sender, EventArgs e)
        {
            Owner.GeneralColumnPanel_MouseEnter(sender, e);
        }

        private void CardControl_MouseLeave(object sender, EventArgs e)
        {
            Owner.GeneralColumnPanel_MouseLeave(sender, e);
        }        

        public void ResizeTo(int cardWidth, int cardHeight)
        {
            if (this.Width == cardWidth && this.Height == cardHeight)
            {
                return;
            }
            this.Width = cardWidth;
            this.Height = cardHeight;
        }

        public bool IsAssignedCard(CardView cardView)
        {
            return _cardView.Equals(cardView);
        }

        public void Redraw(int cardTop)
        {
            if (this.Top == cardTop)
            {
                return;
            }
            this.Location = new Point(0, cardTop);
        }

        private void InitImage()
        {
            Image img = _gameUI.GetCardImage(_cardView.GetCard());
            this.Image = img;
            this.InactivedImage = img;
            if (_cardView.IsBlack())
            {
                this.ActiveImage = img.DrawAsBlueLight();
            }
            else
            {
                this.ActiveImage = img.DrawAsNegative();
            }
            //this.Image = ActiveImage;
        }

        bool _selected = false;
        public void SetActived(bool selected)
        {
            if (selected == false && _selected)
            {
                this.Image = this.InactivedImage;
                this._selected = false;
            }
            if (selected && _selected == false)
            {
                this.Image = this.ActiveImage;
                this._selected = true;
            }
        }
    }
}
