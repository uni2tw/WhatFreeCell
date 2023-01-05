using CoreForm.UI;
using CoreForm.Utilities;
using FreeCellSolitaire.Core.CardModels;
using FreeCellSolitaire.Core.GameModels;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace FreeCellSolitaire.UI
{
    public abstract class GeneralContainer : FlowLayoutPanel
    {
        protected List<GeneralColumnPanel> _columnPanels;
        protected int _cardWidth;
        protected int _cardHeight;
        protected Rectangle _rect;
        protected int _columnNumber;
        protected int _columnSpace;
        protected int _cardSpacing;
        protected IGameUI _gameUI;

        public IGameUI GameUI
        {
            get
            {
                return _gameUI;
            }
        }

        public List<GeneralColumnPanel> GetColumnPanels()
        {
            return _columnPanels;
        }

        public GeneralContainer(IGameUI gameUI, int columnNumber)
        {
            _gameUI = gameUI;
            _columnPanels = new List<GeneralColumnPanel>(columnNumber);
            _columnNumber = columnNumber;

            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
        }

        public abstract int GetCardSpacing();

        public void ResizeTo(Rectangle rect, int dock, int cardWidth, int cardHeight)
        {
            _rect = rect;
            _cardWidth = cardWidth;
            _cardHeight = cardHeight;
            _cardSpacing = GetCardSpacing();
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
                this._columnSpace = (rect.Width - (_cardWidth * _columnNumber)) / (_columnNumber + 1);

                this.Left = rect.Left;
                this.Top = rect.Top + _cardHeight + 12;
                this.Width = rect.Width;
                this.Height = rect.Height - this.Top;                
            }
        }

        public void Clear()
        {
            _columnPanels.ForEach(x => x.Controls.Clear()) ;
            _columnPanels.ForEach(x => x.CardControls.Clear());
        }

        public void RedrawCards(int index, List<CardView> cardViews)
        {
            var columnPanel = _columnPanels[index];
            List<CardView> newCards = new List<CardView>();
            for (int i = 0; i < cardViews.Count; i++)
            {
                var cardControl = columnPanel.GetCardControl(i);
                if (cardControl == null || cardControl.IsAssignedCard(cardViews[i]) == false)
                {
                    newCards = cardViews.Skip(i).ToList();
                    break;
                }
            }
            columnPanel.RemoveCardControlsAfter(cardViews.Count);

            for (int i = 0; i < newCards.Count; i++)
            {
                var card = newCards[i];
                var cardControl = new CardControl(columnPanel, _cardWidth, _cardHeight, card, _gameUI);
                //cardControl.ResizeTo(_cardWidth, _cardHeight);
                cardControl.Click += delegate (object sender, System.EventArgs e)
                {
                    _gameUI.SelectOrMove(((CardControl)sender).Owner.Code);                 
                };
                cardControl.DoubleClick += delegate (object sender, System.EventArgs e)
                {
                    _gameUI.SelectOrMove(((CardControl)sender).Owner.Code);
                };
                columnPanel.AddCardControl(cardControl);
                int cardTop = columnPanel.GetCardControlCount() * _cardSpacing;
                cardControl.Redraw(cardTop);
            }

            for (int i = 0; i < columnPanel.GetCardControlCount(); i++)
            {
                int cardTop = i * _cardSpacing;
                columnPanel.GetCardControl(i).Redraw(cardTop);
                columnPanel.GetCardControl(i).ResizeTo(_cardWidth, _cardHeight);
                columnPanel.GetCardControl(i).SetActived(false);
                columnPanel.GetCardControl(i).Invalidate();
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
        private GeneralContainer _owner;        
        public List<CardControl> CardControls { get; set; }
        public int Index { get; private set; }
        public GeneralColumnPanel(string code, GeneralContainer owner, int index)
        {            
            Code = code;
            Index = index;
            _owner = owner;
            CardControls = new List<CardControl>();

            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer , true);

            this.MouseEnter += GeneralColumnPanel_MouseEnter;
            this.MouseLeave += GeneralColumnPanel_MouseLeave;
        }

        public void GeneralColumnPanel_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = System.Windows.Forms.Cursors.Default;
            //System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
        }

        private Cursor _downArrorCurosr = null;
        public void GeneralColumnPanel_MouseEnter(object sender, EventArgs e)
        {
            if (_owner.GameUI.GetSelectedColumn() != null)
            {
                CardControl selectedCard = _owner.GameUI.GetSelectedColumn().GetCardControl(_owner.GameUI.GetSelectedColumn().GetCardControlCount() - 1);

                Column srcColumn = _owner.GameUI.GetSelectedColumn().GetColumn();
                Column destColumn = null;
                
                if (sender is CardControl)
                {
                    destColumn = ((CardControl)sender).Owner.GetColumn();
                } 
                else 
                {
                    destColumn = _owner.GameUI.GetColumn(this.GetType(), this.Index);                    
                }
                if (destColumn == null)
                {
                    return;
                }

                bool accept = false;
                if (srcColumn.Owner is Tableau && destColumn.Owner is Tableau)
                {
                    int mobility = _owner.GameUI.GetGame().GetExtraMobility(destColumn);
                    var srcCards = srcColumn.GetTableauLinkedCards(mobility);
                    var destCard = destColumn.GetLastCard();
                    var moveableCard = srcCards.FirstOrDefault(x => x.CheckLinkable(destCard, typeof(Tableau)));
                    accept = moveableCard != null;

                    Console.WriteLine(string.Format("{0}{1}-{2}",
                        destColumn.Code, srcColumn.Code, accept ? "was accepted" : "was rejected"));
                }                
                else
                {                    
                    accept = selectedCard.CardView.Moveable(destColumn);
                }

                Console.WriteLine("Moving {0}@{1} to {2} - {3}",
                    selectedCard.CardView.ToNotation(),
                    selectedCard.CardView.Owner.Code,
                    this.Code,
                    Convert.ToInt32(accept));

                if (accept)
                {

                    if (this._owner.GetType() == typeof(TableauContainer))
                    {
                        if (_downArrorCurosr == null)
                        {
                            string resourceName = "freecell_DOWNARROW.cur";
                            Stream resource = GetType().Assembly
                                .GetManifestResourceStream($"FreeCellSolitaire.assets.{resourceName}");
                            _downArrorCurosr = new Cursor(resource);
                        }
                        this.Cursor = _downArrorCurosr;
                    }
                    else
                    {
                        this.Cursor = System.Windows.Forms.Cursors.UpArrow;
                    }
                } 
                else
                {
                    //Arrow is default
                    this.Cursor = System.Windows.Forms.Cursors.Arrow;
                }
            }
            else
            {
                //Arrow is default
                this.Cursor = System.Windows.Forms.Cursors.Arrow;
            }
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

        public Column GetColumn()
        {
            if (CardControls.Count == 0) return null;
            return CardControls[0].CardView.Owner;
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
