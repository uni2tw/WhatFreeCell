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
                int left = (_cardWidth + _cardBorderWidth + _cardBorderWidth) * i;
                int top = 0;
                var panel = new FoundationColumnPanel(i, left, top, _cardWidth, _cardHeight, _cardBorderWidth);
                _columnPanels.Add(panel);
                this.Controls.Add(panel);
            }
        }

        public new void ResizeTo(int left, int top, int ratio)
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
        public FoundationColumnPanel(int index, int left, int top, int cardWidth, int cardHeight, int cardBorderWidth, int ratio = 100)
        {          
            //BackColor = Color.BlueViolet;
            BorderStyle = BorderStyle.FixedSingle;
            //Dock = DockStyle.Fill;
            BorderStyle = BorderStyle.None;            
            this.Paint += delegate (object sender, PaintEventArgs e)
            {
                var self = sender as Panel;
                ControlPaint.DrawBorder(e.Graphics, self.ClientRectangle, Color.Green, ButtonBorderStyle.Inset);
            };            

            ResizeTo(left, top, cardWidth, cardHeight, cardBorderWidth, ratio);
        }

        public void ResizeTo(int left, int top, int cardWidth, int cardHeight, int cardBorderWidth, int ratio)
        {
            Location = new Point(left, top);
            Width = cardWidth + cardBorderWidth + cardBorderWidth;
            Height = cardHeight + cardBorderWidth + cardBorderWidth;
        }
    }

    public class HomecellsContainer
    {
        private List<HomecellColumnPanel> _columnContainers;
        private IGameForm form;

        public HomecellsContainer()
        {
            _columnContainers = new List<HomecellColumnPanel>(4);
        }

        public HomecellsContainer(IGameForm form)
        {
            this.form = form;
        }
    }
}
