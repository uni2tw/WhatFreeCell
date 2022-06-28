using FreeCellSolitaire.Core.CardModels;

namespace FreeCellSolitaire.Core.GameModels
{
    /// <summary>
    /// 下方待處理區
    /// </summary>
    public class Tableau : IZone
    {
        private IGame _game;
        public Tableau(IGame game)
        {
            this._game = game;
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
        public int ColumnCapacity
        {
            get
            {
                return 13;
            }
        }

        public Column[] _columns = new Column[4];

        public void Init(Deck deck)
        {
            _columns[0] = new Column(this, 0, ColumnCapacity);
            _columns[1] = new Column(this, 1, ColumnCapacity);
            _columns[2] = new Column(this, 2, ColumnCapacity);
            _columns[3] = new Column(this, 3, ColumnCapacity);

            int n = 0;
            while (true)
            {
                var card = deck.Pick();
                _columns[n % 4].AddCards(card);
                if (card == null)
                {
                    break;
                }
                n++;
            }            
        }

        public void DebugInfo()
        {
            Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            Console.WriteLine(_columns[0].ToString());
            Console.WriteLine(_columns[1].ToString());
            Console.WriteLine(_columns[2].ToString());
            Console.WriteLine(_columns[3].ToString());
        }


        public bool MoveCard(int sourceIndex, IZone target, int targetIndex)
        {
            return true;
        }

        public override string ToString()
        {
            return this.GetType().Name;
        }

    }
}

