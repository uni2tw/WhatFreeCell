using CoreForm.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace FreeCellSolitaire.UI
{
    public class FoundationsContainer : Panel
    {
        private List<FoundationColumnPanel> _columnPanels;
        private IGameForm _form;
        private int _columnNumber;
        private int _cardWidth;
        private int _cardHeight;
        private int _cardBorderWidth = 1;

        public FoundationsContainer(IGameForm form, int columnNumber, int left, int top, int cardWidth, int cardHeight)
        {            
            this._form = form;
            _columnNumber = columnNumber;
            _cardWidth = cardWidth;
            _cardHeight = cardHeight;
            _columnPanels = new List<FoundationColumnPanel>(columnNumber);
            this.BorderStyle = BorderStyle.None;
                        
            this.Name = nameof(FoundationsContainer);
                     
            Resize(left, top, cardWidth, cardHeight);
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

        public void Resize(int left, int top, int cardWidth, int cardHeight)
        {
            int right = left + (cardWidth + _cardBorderWidth + _cardBorderWidth) * 4;
            int bottom = top + cardHeight + _cardBorderWidth + _cardBorderWidth;

            this.Left = left;
            this.Top = top;
            this.Width = right - this.Left;
            this.Height = bottom - this.Top;
            this._form.SetControlReady(this);         
        }
    }

    public class FoundationColumnPanel : GeneralColumnPanel
    {
        public FoundationColumnPanel(int index, int left, int top, int cardWidth, int cardHeight, int cardBorderWidth)
        {
            Location = new Point(left, top);
            Width = cardWidth + cardBorderWidth + cardBorderWidth;
            Height = cardHeight + cardBorderWidth + cardBorderWidth;            
            //BackColor = Color.BlueViolet;
            BorderStyle = BorderStyle.FixedSingle;
            //Dock = DockStyle.Fill;
            BorderStyle = BorderStyle.None;
            this.Paint += delegate (object sender, PaintEventArgs e)
            {
                var self = sender as Panel;
                ControlPaint.DrawBorder(e.Graphics, self.ClientRectangle, Color.Green, ButtonBorderStyle.Inset);
            };
        }
    }

}
