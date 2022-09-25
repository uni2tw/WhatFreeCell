using CoreForm.UI;
using FreeCellSolitaire.Core.CardModels;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FreeCellSolitaire.UI
{
    public abstract class GeneralContainer : FlowLayoutPanel
    {
        protected List<GeneralColumnPanel> _columnPanels;
        protected int _cardWidth;
        protected int _cardHeight;
        protected int _columnNumber;
        protected int _columnSpace;
        protected int _cardSpacing;
        protected IGameUI _gameUI;
        public GeneralContainer(IGameUI gameUI, int cardWidth, int cardHeight, int columnNumber)
        {
            _gameUI = gameUI;
            _columnPanels = new List<GeneralColumnPanel>(columnNumber);
            _cardWidth = cardWidth;
            _cardHeight = cardHeight;
            _columnNumber = columnNumber;
            _cardSpacing = GetCardSpacing();
        }

        public abstract int GetCardSpacing();

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
        }

        public void Clear()
        {
            _columnPanels.ForEach(x => x.Controls.Clear()) ;
            _columnPanels.ForEach(x => x.CardControls.Clear());
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
                var cardControl = new CardControl(columnPanel, _cardWidth, _cardHeight, card);                
                cardControl.Click += delegate (object sender, System.EventArgs e)
                {
                    _gameUI.SelectColumn(((CardControl)sender).Owner.Code, false);
                };
                cardControl.DoubleClick += delegate (object sender, System.EventArgs e)
                {
                    _gameUI.SelectColumn(((CardControl)sender).Owner.Code, true);
                };
                columnPanel.AddCardControl(cardControl);
                int cardTop = columnPanel.GetCardControlCount() * _cardSpacing;
                cardControl.Redraw(cardTop);
            }

            for (int i = 0; i < columnPanel.GetCardControlCount(); i++)
            {
                columnPanel.GetCardControl(i).SetActived(false);
            }
        }

        public void SetActiveColumn(string activeColumnCode)
        {
            foreach (var columnPanel in _columnPanels)
            {
                if (columnPanel.Code == activeColumnCode && columnPanel.GetCardControlCount() > 0)
                {
                    columnPanel.GetCardControl(columnPanel.GetCardControlCount() - 1).SetActived(true);
                }
            }
        }
    }

    public class GeneralColumnPanel : Panel
    {
        public string Code { get; private set; }
        public List<CardControl> CardControls { get; set; }
        public GeneralColumnPanel(string code)
        {
            Code = code;
            CardControls = new List<CardControl>();
        }        
        public void AddCardControl(CardControl cardControl)
        {
            cardControl.SetIndex(CardControls.Count);
            CardControls.Add(cardControl);
            
            this.Controls.Add(cardControl);            
            cardControl.BringToFront();
        }
        public void RemoveCardControlsAfter(int index)
        {            
            while (this.Controls.Count > index)
            {
                var cardControl = CardControls[index];
                CardControls.Remove(cardControl);
                this.Controls.Remove(cardControl);
            }
        }
        public int GetCardControlCount()
        {
            return CardControls.Count;
        }

        public CardControl GetCardControl(int i)
        {
            if (CardControls.Count > i)
            {
                return CardControls[i];
            }
            return null;
        }
    }

}
