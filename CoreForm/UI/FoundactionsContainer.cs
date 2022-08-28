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
        int _columnNumber;         
        int _cardBorderWidth = 1;
        int _ratio = 100;

        public FoundationsContainer(IGameForm form, int columnNumber,
            Rectangle rect, int dock, int cardWidth, int cardHeight, int ratio = 100)
            :base(form, cardWidth, cardHeight, columnNumber)
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
                var panel = new FoundationColumnPanel(_cardWidth, _cardHeight, _cardBorderWidth);
                _columnPanels.Add(panel);
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
            Width = cardWidth;
            Height = cardHeight;
        }
    }

    public class HomecellsContainer : GeneralContainer
    {        
        int _columnNumber;
        int _cardBorderWidth = 1;
        int _ratio = 100;

        public HomecellsContainer(IGameForm form, int columnNumber,
            Rectangle rect, int dock, int cardWidth, int cardHeight, int ratio = 100)
            : base(form, cardWidth, cardHeight, columnNumber)
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
                var panel = new HomecellColumnPanel(_cardWidth, _cardHeight, _cardBorderWidth);                
                _columnPanels.Add(panel);
                this.Controls.Add(panel);
            }
        }

        public override int GetCardSpacing()
        {
            return 0;
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
            Width = cardWidth;
            Height = cardHeight;
        }
    }
}
