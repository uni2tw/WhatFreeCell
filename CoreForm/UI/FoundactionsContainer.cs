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
    public class TableauContainer : GeneralContainer
    {

        List<TableauColumnPanel> _columnPanels;
        Rectangle _rect;
        int _columnNumber;
        int _cardBorderWidth = 1;
        int _ratio = 100;        

        public TableauContainer(IGameForm form, int columnNumber,
            Rectangle rect, int dock, int cardWidth, int cardHeight, int ratio = 100)
            : base(form, cardWidth, cardHeight, columnNumber)
        {
            _columnNumber = columnNumber;            
            _columnPanels = new List<TableauColumnPanel>(columnNumber);
            _rect = rect;
            this.BorderStyle = BorderStyle.None;
            this.Name = nameof(FoundationsContainer);
            
            ResizeTo(rect, dock, ratio);
            SetControls();
        }

        public void SetControls()
        {
            this.Resize += delegate (object sender, EventArgs e)
            {
                _columnPanels.ForEach(x => x.Height = this.Height);
            };
            for (int i = 0; i < _columnNumber; i++)
            {
                var panel = new TableauColumnPanel(i, _cardWidth, _cardHeight, _cardBorderWidth, _columnSpace, _rect);
                _columnPanels.Add(panel);
                this.Controls.Add(panel);
            }
        }

        public void RedrawCards(int index, List<Card> cards)
        {
            var columnPanel = _columnPanels[index];
            List<Card> newCards = new List<Card>();
            for (int i = 0; i < cards.Count; i++)
            {
                var cardControl = columnPanel.GetCardControl(i);
                if (cardControl == null || cardControl.IsAssignedCard(cards[i]) == false)
                {
                    newCards = cards.Skip(i).ToList();
                    break;
                }
            }
            columnPanel.RemoveCardControlsAfter(cards.Count);

            for (int i = 0; i < newCards.Count; i++)
            {
                var card = newCards[i];
                var cardControl = new CardControl(_cardWidth, _cardHeight, card);
                columnPanel.AddCardControl(cardControl);
                cardControl.Redraw();
            }
        }
    }

    public class TableauColumnPanel : GeneralColumnPanel
    {        
        public TableauColumnPanel(int index, int cardWidth, int cardHeight, 
            int cardBorderWidth, int columnSpace,
            Rectangle rectParent,
            int ratio = 100)
        {

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
            this.Left = ((index + 1) * columnSpace) + (index * cardWidth);
            BorderStyle = BorderStyle.None;
            Dock = DockStyle.None;
        }
    }

    public class GeneralContainer : Panel
    {
        protected IGameForm _form;        
        protected int _cardWidth;
        protected int _cardHeight;
        protected int _columnNumber;
        protected int _columnSpace;
        public GeneralContainer(IGameForm form, int cardWidth, int cardHeight, int columnNumber)
        {
            _form = form;            
            _cardWidth = cardWidth;
            _cardHeight = cardHeight;
            _columnNumber = columnNumber;
        }

        public void ResizeTo(Rectangle rect, int dock, int ratio)
        {
            if (dock == 1)
            {
                this.Left = rect.Left;
                this.Top = rect.Top;
                this.Width = _cardWidth * _columnNumber;
                this.Height = _cardHeight;
            }
            else if (dock == 2)
            {
                this.Left = rect.Right - (_cardWidth * 4);
                this.Top = rect.Top;
                this.Width = _cardWidth * _columnNumber;
                this.Height = _cardHeight;
            } 
            else if (dock == 3)
            {
                this.Left = rect.Left;
                this.Top = rect.Top + _cardHeight + 12;
                this.Width = rect.Width;
                this.Height = rect.Height - this.Top;                
                this._columnSpace = (this.Width - (_cardWidth * _columnNumber)) / (_columnNumber + 1);
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
                var panel = new FoundationColumnPanel(_cardWidth, _cardHeight, _cardBorderWidth);
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
                var panel = new HomecellColumnPanel(_cardWidth, _cardHeight, _cardBorderWidth);
                _columnPanels.Add(panel);
                this.Controls.Add(panel);
            }
        }
    }


}
