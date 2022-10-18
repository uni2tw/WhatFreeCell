using CoreForm.UI;
using FreeCellSolitaire.Core.CardModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace FreeCellSolitaire.UI
{
    public class FoundationsContainer : GeneralContainer
    {
        //max number of columns
        int _columnNumber;
        int _cardBorderWidth = 1;
        int _ratio = 100;
        public FoundationsContainer(IGameUI gameUI, int columnNumber,
            Rectangle rect, int dock, int cardWidth, int cardHeight)
            : base(gameUI, columnNumber)
        {
            _columnNumber = columnNumber;

            this.BorderStyle = BorderStyle.None;
            this.Name = nameof(FoundationsContainer);

            ResizeTo(rect, dock, cardWidth, cardHeight);
            SetControls();
        }


        public void SetControls()
        {
            for (int i = 0; i < _columnNumber; i++)
            {
                var panel = new FoundationColumnPanel(_cardWidth, _cardHeight, _cardBorderWidth, $"f{i}", this);
                _columnPanels.Add(panel);
                panel.Click += delegate (object sender, System.EventArgs e)
                {
                    _gameUI.SelectOrMove((sender as FoundationColumnPanel).Code);
                };
                this.Controls.Add(panel);
            }
            this.Resize += delegate (object sender, System.EventArgs e)
            {
                foreach (var panel in _columnPanels)
                {
                    (panel as FoundationColumnPanel).ResizeTo(_cardWidth, _cardHeight, _cardBorderWidth);
                }
            };
        }
        public override int GetCardSpacing()
        {
            return 0;
        }

    }

    public class FoundationColumnPanel : GeneralColumnPanel
    {
        public FoundationColumnPanel(int cardWidth, int cardHeight, int cardBorderWidth, string code, GeneralContainer owner)
            : base(code, owner)
        {
            BorderStyle = BorderStyle.None;
            this.Margin = new Padding(0);

            this.Paint += delegate (object sender, PaintEventArgs e)
            {
                var self = sender as Panel;
                ControlPaint.DrawBorder(e.Graphics, self.ClientRectangle, Color.Green, ButtonBorderStyle.Inset);
            };

            ResizeTo(cardWidth, cardHeight, cardBorderWidth);
        }

        public void ResizeTo(int cardWidth, int cardHeight, int cardBorderWidth)
        {
            Width = cardWidth;
            Height = cardHeight;
            this.Invalidate();
        }
    }

}
