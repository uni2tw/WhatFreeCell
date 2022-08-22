using CoreForm.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace FreeCellSolitaire.UI
{
    public class FoundationsContainer : Panel
    {
        List<FoundationColumnPanel> _columnPanels;
        IGameForm _form;
        int _columnNumber;
        int _top;
        int _left;
        int _cardWidth;
        int _cardHeight;
        int _cardBorderWidth = 1;
        int _ratio = 100;

        public FoundationsContainer(IGameForm form, int columnNumber,
            int left, int top, int cardWidth, int cardHeight, int ratio = 100)
        {            
            this._form = form;
            _columnNumber = columnNumber;
            _cardWidth = cardWidth;
            _cardHeight = cardHeight;

            _columnPanels = new List<FoundationColumnPanel>(columnNumber);
            this.BorderStyle = BorderStyle.None;                        
            this.Name = nameof(FoundationsContainer);
            
           
            SetControls();

            ResizeTo(left, top, ratio);
        }

        public void SetControls()
        {
            for (int i = 0; i < _columnNumber; i++)
            {
                var panel = new FoundationColumnPanel(_cardWidth, _cardHeight, _cardBorderWidth);
                _columnPanels.Add(panel);
                this.Controls.Add(panel);
            }
        }

        public void ResizeTo(int left, int top, int ratio)
        {
            _left = left;
            _top = top;
            _ratio = ratio;
            
            int right = _left + (_cardWidth + _cardBorderWidth + _cardBorderWidth) * 4;
            int bottom = _top + _cardHeight + _cardBorderWidth + _cardBorderWidth;

            this.Left = _left;
            this.Top = _top;
            this.Width = right - _left;
            this.Height = bottom - _top;

            if (ratio > 100)
            {
                this.Width = (int)(this.Width * ratio / 100);
                this.Height = (int)(this.Height * ratio / 100);
            }

            foreach (var columnPanel in _columnPanels)
            {

            }

            this._form.SetControlReady(this);         
        }
    }

    public class FoundationColumnPanel : GeneralColumnPanel
    {
        public FoundationColumnPanel(int cardWidth, int cardHeight, int cardBorderWidth, int ratio = 100)
        {          
            //BackColor = Color.BlueViolet;
            BorderStyle = BorderStyle.FixedSingle;
            Dock = DockStyle.Left;
            BorderStyle = BorderStyle.None;            
            this.Paint += delegate (object sender, PaintEventArgs e)
            {
                var self = sender as Panel;
                ControlPaint.DrawBorder(e.Graphics, self.ClientRectangle, Color.Green, ButtonBorderStyle.Inset);
            };            

            ResizeTo(cardWidth, cardHeight, cardBorderWidth, ratio);
        }

        public void ResizeTo(int cardWidth, int cardHeight, int cardBorderWidth, int ratio)
        {
            //Width = cardWidth + cardBorderWidth + cardBorderWidth;
            //Height = cardHeight + cardBorderWidth + cardBorderWidth;
            Width = cardWidth;
            Height = cardHeight;
        }
    }

    public class HomecellColumnPanel : GeneralColumnPanel
    {
        public HomecellColumnPanel(int cardWidth, int cardHeight, int cardBorderWidth, int ratio = 100)
        {
            //BackColor = Color.BlueViolet;
            BorderStyle = BorderStyle.FixedSingle;
            Dock = DockStyle.Left;
            BorderStyle = BorderStyle.None;
            this.Paint += delegate (object sender, PaintEventArgs e)
            {
                var self = sender as Panel;
                ControlPaint.DrawBorder(e.Graphics, self.ClientRectangle, Color.Green, ButtonBorderStyle.Inset);
            };

            ResizeTo(cardWidth, cardHeight, cardBorderWidth, ratio);
        }

        public void ResizeTo(int cardWidth, int cardHeight, int cardBorderWidth, int ratio)
        {
            //Width = cardWidth + cardBorderWidth + cardBorderWidth;
            //Height = cardHeight + cardBorderWidth + cardBorderWidth;
            Width = cardWidth;
            Height = cardHeight;
        }
    }

    public class HomecellsContainer : Panel
    {
        List<HomecellColumnPanel> _columnPanels;
        IGameForm _form;
        int _columnNumber;
        int _top;
        int _left;
        int _cardWidth;
        int _cardHeight;
        int _cardBorderWidth = 1;
        int _ratio = 100;

        public HomecellsContainer(IGameForm form, int columnNumber,
            int left, int top, int cardWidth, int cardHeight, int ratio = 100)
        {
            this._form = form;
            _columnNumber = columnNumber;
            _cardWidth = cardWidth;
            _cardHeight = cardHeight;

            _columnPanels = new List<HomecellColumnPanel>(columnNumber);
            this.BorderStyle = BorderStyle.None;
            this.Name = nameof(FoundationsContainer);

            SetControls();

            ResizeTo(left, top, ratio);
        }

        public void SetControls()
        {
            for (int i = 0; i < _columnNumber; i++)
            {
                var panel = new HomecellColumnPanel(_cardWidth, _cardHeight, _cardBorderWidth);
                _columnPanels.Add(panel);
                this.Controls.Add(panel);
            }
        }

        public void ResizeTo(int left, int top, int ratio)
        {
            _left = left;
            _top = top;
            _ratio = ratio;

            int right = _left + (_cardWidth + _cardBorderWidth + _cardBorderWidth) * 4;
            int bottom = _top + _cardHeight + _cardBorderWidth + _cardBorderWidth;

            this.Left = _left;
            this.Top = _top;
            this.Width = right - _left;
            this.Height = bottom - _top;

            if (ratio > 100)
            {
                this.Width = (int)(this.Width * ratio / 100);
                this.Height = (int)(this.Height * ratio / 100);
            }

            foreach (var columnPanel in _columnPanels)
            {

            }

            this._form.SetControlReady(this);
        }
    }
}
