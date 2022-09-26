using CoreForm.UI;
using System.Drawing;
using System.Windows.Forms;

namespace FreeCellSolitaire.UI
{
    public class HomecellsContainer : GeneralContainer
    {        
        int _columnNumber;
        int _cardBorderWidth = 1;
        int _ratio = 100;

        public HomecellsContainer(IGameUI gameUI, int columnNumber,
            Rectangle rect, int dock, int cardWidth, int cardHeight, int ratio = 100)
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
                var panel = new HomecellColumnPanel(_cardWidth, _cardHeight, _cardBorderWidth, $"h{i}");
                panel.Click += delegate (object sender, System.EventArgs e)
                {
                    _gameUI.SelectColumn((sender as GeneralColumnPanel).Code, false);
                };
                _columnPanels.Add(panel);
                this.Controls.Add(panel);
            }
            this.Resize += delegate (object sender, System.EventArgs e)
            {
                foreach(var panel in _columnPanels)
                {
                    (panel as HomecellColumnPanel).ResizeTo(_cardWidth, _cardHeight, _cardBorderWidth);
                }
            };
        }

        public override int GetCardSpacing()
        {
            return 0;
        }
    }

    public class HomecellColumnPanel : GeneralColumnPanel
    {
        public HomecellColumnPanel(int cardWidth, int cardHeight, int cardBorderWidth, string code)
            :base(code)
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
            this.Refresh();
        }
    }
}
