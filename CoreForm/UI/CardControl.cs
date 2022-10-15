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
        IGameUI _gameUI;
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

        public CardControl(GeneralColumnPanel owner, int cardWidth, int cardHeight, Card card, IGameUI gameUI)
        {
            _card = card;
            _gameUI = gameUI;
            Owner = owner;
            
            this.BorderStyle = BorderStyle.None;
            this.BackColor = Color.Black;
            this.Margin = new Padding(0);
            this.SizeMode = PictureBoxSizeMode.StretchImage;

            ResizeTo(cardWidth, cardHeight);
            InitImage();            
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

        public bool IsAssignedCard(Card card)
        {
            return _card.Equals(card);
        }

        public void Redraw(int cardTop)
        {
            if (this.Top == cardTop) {
                return;
            }
            this.Location = new Point(0, cardTop);            
        }

        private void InitImage()
        {            
            Image img = _gameUI.GetCardImage(_card);         
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
