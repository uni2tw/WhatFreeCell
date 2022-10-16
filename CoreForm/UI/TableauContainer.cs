using CoreForm.UI;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace FreeCellSolitaire.UI
{
    public class TableauContainer : GeneralContainer
    {        
        int _columnNumber;
        int _cardBorderWidth = 1;
        int _ratio = 100;        

        public TableauContainer(IGameUI gameUI, int columnNumber,
            Rectangle rect, int dock, int cardWidth, int cardHeight, int ratio = 100)
            : base(gameUI, columnNumber)
        {
            _columnNumber = columnNumber;                                    
            this.BorderStyle = BorderStyle.None;
            this.Name = nameof(FoundationsContainer);   
            
            ResizeTo(rect, dock, cardWidth, cardHeight);
            SetControls();
        }

        public override int GetCardSpacing()
        {
            return (int)(_cardHeight / 6.1f);
        }

        public void SetControls()
        {
            for (int i = 0; i < _columnNumber; i++)
            {
                var panel = new TableauColumnPanel(i, _cardWidth, _cardHeight, _cardBorderWidth, _columnSpace, _rect, $"t{i}");
                panel.Click += delegate (object sender, System.EventArgs e)
                {
                    _gameUI.SelectOrMove((sender as GeneralColumnPanel).Code);
                };
                _columnPanels.Add(panel);
                this.Controls.Add(panel);
            }
            this.Resize += delegate (object sender, EventArgs e)
            {                
                foreach (var panel in _columnPanels)
                {
                    (panel as TableauColumnPanel).ResizeTo(
                        _columnPanels.IndexOf(panel), _cardWidth, _cardHeight, _cardBorderWidth, _columnSpace, _rect.Height);
                }
            };
        }

    }

    public class TableauColumnPanel : GeneralColumnPanel
    {
        public TableauColumnPanel(int index, int cardWidth, int cardHeight,
            int cardBorderWidth, int columnSpace,
            Rectangle rectParent, string code) : base(code)
        {            
            BorderStyle = BorderStyle.None;
            this.Paint += delegate (object sender, PaintEventArgs e)
            {
                var self = sender as Panel;
                //for debug 
                //ControlPaint.DrawBorder(e.Graphics, self.ClientRectangle, Color.Green, ButtonBorderStyle.Inset);
            };

            ResizeTo(index, cardWidth, cardHeight, cardBorderWidth, columnSpace, rectParent.Height);
        }

        public void ResizeTo(int index, int cardWidth, int cardHeight, int cardBorderWidth, int columnSpace,
            int height)
        {
            this.Width = cardWidth;
            this.Height = height;                  
            if (index == 0)
            {
                this.Margin = new Padding(columnSpace, 0, columnSpace / 2, 0);
            }
            else
            {
                this.Margin = new Padding(columnSpace / 2, 0, columnSpace / 2, 0);
            }
            Dock = DockStyle.None;
            this.Invalidate();
        }
    }


}
