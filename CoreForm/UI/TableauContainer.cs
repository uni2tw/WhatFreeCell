using CoreForm.UI;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace FreeCellSolitaire.UI
{
    public class TableauContainer : GeneralContainer
    {
        Rectangle _rect;
        int _columnNumber;
        int _cardBorderWidth = 1;
        int _ratio = 100;        

        public TableauContainer(IGameUI gameUI, int columnNumber,
            Rectangle rect, int dock, int cardWidth, int cardHeight, int ratio = 100)
            : base(gameUI, cardWidth, cardHeight, columnNumber)
        {
            _columnNumber = columnNumber;                        
            _rect = rect;            
            this.BorderStyle = BorderStyle.None;
            this.Name = nameof(FoundationsContainer);                        
            ResizeTo(rect, dock, ratio);
            SetControls();
        }

        public override int GetCardSpacing()
        {
            return (int)(_cardHeight / 6.1f);
        }

        public void SetControls()
        {
            this.Resize += delegate (object sender, EventArgs e)
            {
                _columnPanels.ForEach(x => x.Height = this.Height);
            };
            for (int i = 0; i < _columnNumber; i++)
            {
                var panel = new TableauColumnPanel(i, _cardWidth, _cardHeight, _cardBorderWidth, _columnSpace, _rect, $"t{i}");
                panel.Click += delegate (object sender, System.EventArgs e)
                {
                    _gameUI.SelectColumn((sender as GeneralColumnPanel).Code);
                };
                _columnPanels.Add(panel);
                this.Controls.Add(panel);
            }
        }

    }

    public class TableauColumnPanel : GeneralColumnPanel
    {
        public TableauColumnPanel(int index, int cardWidth, int cardHeight,
            int cardBorderWidth, int columnSpace,
            Rectangle rectParent, string code,
            int ratio = 100) : base(code)
        {            
            BorderStyle = BorderStyle.None;
            this.Paint += delegate (object sender, PaintEventArgs e)
            {
                var self = sender as Panel;
                //ControlPaint.DrawBorder(e.Graphics, self.ClientRectangle, Color.Green, ButtonBorderStyle.Inset);
            };

            ResizeTo(index, cardWidth, cardHeight, cardBorderWidth, columnSpace, rectParent.Height, ratio);
        }

        public void ResizeTo(int index, int cardWidth, int cardHeight, int cardBorderWidth, int columnSpace,
            int height,
            int ratio)
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
        }
    }


}
