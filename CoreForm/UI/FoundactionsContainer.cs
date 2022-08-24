using CoreForm.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace FreeCellSolitaire.UI
{
    public class TableauContainer : GeneralContainer
    {

        List<TableauColumnPanel> _columnPanels;
        int _columnNumber;
        int _cardBorderWidth = 1;
        int _ratio = 100;        

        public TableauContainer(IGameForm form, int columnNumber,
            Rectangle rect, int dock, int cardWidth, int cardHeight, int ratio = 100)
            : base(form, cardWidth, cardHeight, columnNumber)
        {
            _columnNumber = columnNumber;            
            _columnPanels = new List<TableauColumnPanel>(columnNumber);
            this.BorderStyle = BorderStyle.None;
            this.Name = nameof(FoundationsContainer);

            
            ResizeTo(rect, dock, ratio);
            SetControls();
        }

        public void SetControls()
        {
            for (int i = 0; i < _columnNumber; i++)
            {
                var panel = new TableauColumnPanel(_cardRect.Width, _cardRect.Height, _cardBorderWidth);
                _columnPanels.Add(panel);
                this.Controls.Add(panel);
            }
        }
    }

    public class TableauColumnPanel : GeneralColumnPanel
    {
        public TableauColumnPanel(int cardWidth, int cardHeight, int cardBorderWidth, int ratio = 100)
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

    public class GeneralContainer : Panel
    {
        protected IGameForm _form;
        protected Rectangle _cardRect;
        protected int _columnNumber;
        protected int _columnSpace;
        public GeneralContainer(IGameForm form, int cardWidth, int cardHeight, int columnNumber)
        {
            _form = form;
            _cardRect = new Rectangle(0, 0, cardWidth, cardHeight);            
            _columnNumber = columnNumber;
        }

        public void ResizeTo(Rectangle rect, int dock, int ratio)
        {
            if (dock == 1)
            {
                this.Left = rect.Left;
                this.Top = rect.Top;
                this.Width = _cardRect.Width * _columnNumber;
                this.Height = _cardRect.Height;
            }
            else if (dock == 2)
            {
                this.Left = rect.Right - (_cardRect.Width * 4);
                this.Top = rect.Top;
                this.Width = _cardRect.Width * _columnNumber;
                this.Height = _cardRect.Height;
            } 
            else if (dock == 3)
            {
                this.Left = rect.Left;
                this.Top = rect.Top + _cardRect.Height + 12;
                this.Width = rect.Width;
                this.Height = rect.Height - this.Top;
                this.BorderStyle = BorderStyle.FixedSingle;
                this._columnSpace = (this.Width - (_cardRect.Width * _columnNumber)) / (_columnNumber + 1);
            }

            if (ratio > 100)
            {
                this.Width = (int)(this.Width * ratio / 100);
                this.Height = (int)(this.Height * ratio / 100);
            }

    
            this._form.SetControlReady(this);
        }
    }
    public class FoundationsContainer : GeneralContainer
    {
        List<FoundationColumnPanel> _columnPanels;        
        int _columnNumber;         
        int _cardBorderWidth = 1;
        int _ratio = 100;

        public FoundationsContainer(IGameForm form, int columnNumber,
            Rectangle rect, int dock, int cardWidth, int cardHeight, int ratio = 100)
            :base(form, cardWidth, cardHeight, columnNumber)
        {                        
            _columnNumber = columnNumber;

            _columnPanels = new List<FoundationColumnPanel>(columnNumber);
            this.BorderStyle = BorderStyle.None;                        
            this.Name = nameof(FoundationsContainer);            
           
            SetControls();

            ResizeTo(rect, dock, ratio);
        }

        public void SetControls()
        {
            for (int i = 0; i < _columnNumber; i++)
            {                
                var panel = new FoundationColumnPanel(_cardRect.Width, _cardRect.Height, _cardBorderWidth);
                _columnPanels.Add(panel);
                this.Controls.Add(panel);
            }
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

    public class HomecellsContainer : GeneralContainer
    {
        List<HomecellColumnPanel> _columnPanels;        
        int _columnNumber;
        int _cardBorderWidth = 1;
        int _ratio = 100;

        public HomecellsContainer(IGameForm form, int columnNumber,
            Rectangle rect, int dock, int cardWidth, int cardHeight, int ratio = 100)
            : base(form, cardWidth, cardHeight, columnNumber)
        {
            _columnNumber = columnNumber;

            _columnPanels = new List<HomecellColumnPanel>(columnNumber);
            this.BorderStyle = BorderStyle.None;
            this.Name = nameof(FoundationsContainer);

            SetControls();

            ResizeTo(rect, dock, ratio);
        }

        public void SetControls()
        {
            for (int i = 0; i < _columnNumber; i++)
            {
                var panel = new HomecellColumnPanel(_cardRect.Width, _cardRect.Height, _cardBorderWidth);
                _columnPanels.Add(panel);
                this.Controls.Add(panel);
            }
        }

    }
}
