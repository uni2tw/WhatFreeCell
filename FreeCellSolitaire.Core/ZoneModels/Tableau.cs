using System.Linq;
using FreeCellSolitaire.Core.CardModels;

namespace FreeCellSolitaire.Core.GameModels
{
    /// <summary>
    /// 下方待處理區
    /// </summary>
    public class Tableau : IZone
    {
        IGame _game;
        Column[] _columns;
        public Tableau(IGame game)
        {
            this._game = game;
            if (game != null)
            {
                this._game.Tableau = this;
            }
            this.Init();
        }

        public IGame Game
        {
            get
            {
                return _game;
            }
        }

        private void Init()
        {
            _columns = new Column[ColumnCount];
            for (int i = 0; i < ColumnCount; i++)
            {
                _columns[i] = new Column(this, i, ColumnCapacity);
            }
        }

        public bool CanInternalSwap
        {
            get
            {
                return true;
            }
        }
        public bool CanMoveIn
        {
            get
            {
                return true;
            }
        }

        public bool CanMoveOut
        {
            get
            {
                return true;
            }
        }
        public int ColumnCapacity => 13;

        public int ColumnCount => 8;

        public void Init(Deck deck)
        {            
            _columns = new Column[ColumnCount];
            for (int i=0;i< ColumnCount; i++)
            {
                _columns[i] = new Column(this, i, ColumnCapacity);
            }
            if (deck == null) return;
            int n = 0;
            while (true)
            {
                var card = deck.Pick();
                if (card == null)
                {
                    break;
                }
                _columns[n % ColumnCount].AddCards(card);
                n++;
            }            
        }

        public void DebugInfo(bool timeStamp = true)
        {
            if (timeStamp)
            {
                Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            } 
            for (int i = 0; i < ColumnCount; i++)
            {
                Console.WriteLine($"tableau[{i}]:{_columns[i]}");
            }
        }

        public void DebugColumnInfo(int columnIndex)
        {
            Console.WriteLine($"tableau[{columnIndex}]:{_columns[columnIndex]}");
        }

        public bool WasEmpty()
        {
            return _columns.All(x => x.GetLastCard() == null);
        }

        public bool MoveCard(int sourceIndex, IZone target, int targetIndex)
        {
            return true;
        }

        public override string ToString()
        {
            return this.GetType().Name;
        }

        public Column GetColumn(int columnIndex)
        {
            return _columns[columnIndex];
        }

        public CardView GetCard(int columnIndex, int? cardIndex = null)
        {
            if (cardIndex == null)
            {
                cardIndex = GetColumn(columnIndex).GetCardsCount() - 1;
            }
            var card = GetColumn(columnIndex).GetCard(cardIndex.Value);
            return card;
        }

    }
}

