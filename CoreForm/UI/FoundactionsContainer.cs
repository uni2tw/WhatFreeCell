using CoreForm.UI;
using FreeCellSolitaire.Core.CardModels;
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
            Rectangle rect, int dock, int cardWidth, int cardHeight, int ratio = 100)
            :base(gameUI, cardWidth, cardHeight, columnNumber)
        {
            _columnNumber = columnNumber;

            this.BorderStyle = BorderStyle.None;                        
            this.Name = nameof(FoundationsContainer);            
           
            SetControls();

            ResizeTo(rect, dock, ratio);
        }


        public void SetControls()
        {
            for (int i = 0; i < _columnNumber; i++)
            {                
                var panel = new FoundationColumnPanel(_cardWidth, _cardHeight, _cardBorderWidth,$"f{i}");
                _columnPanels.Add(panel);
                panel.Click += delegate (object sender, System.EventArgs e)
                {
                    _gameUI.SelectColumn((sender as FoundationColumnPanel).Code);                    
                };
                this.Controls.Add(panel);
            }
        }
        public override int GetCardSpacing()
        {
            return 0;
        }

    }

    public class FoundationColumnPanel : GeneralColumnPanel
    {
        public FoundationColumnPanel(int cardWidth, int cardHeight, int cardBorderWidth, string code, int ratio = 100)
            :base(code)
        {
            BorderStyle = BorderStyle.None;
            this.Margin = new Padding(0);
            
            this.Paint += delegate (object sender, PaintEventArgs e)
            {
                var self = sender as Panel;
                ControlPaint.DrawBorder(e.Graphics, self.ClientRectangle, Color.Green, ButtonBorderStyle.Inset);
            };            

            ResizeTo(cardWidth, cardHeight, cardBorderWidth, ratio);
        }

        public void ResizeTo(int cardWidth, int cardHeight, int cardBorderWidth, int ratio)
        {
            Width = cardWidth;
            Height = cardHeight;
        }
    }

}
