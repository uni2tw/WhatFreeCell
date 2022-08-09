using FreeCellSolitaire.Core.GameModels;

namespace FreeCellSolitaire.Entities.GameEntities
{
    /// <summary>
    /// 右上完成區
    /// </summary>
    public class Homecells : IZone
    {
        IGame _game;
        Column[] _columns;

        /// <summary>
        /// 初始 右上完成區
        /// </summary>
        /// <param name="form"></param>
        public Homecells(IGame game)
        {
            this._game = game;
            if (game != null)
            {
                this._game.Homecells = this;
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
                return false;
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
                return false;
            }
        }
        public int ColumnCapacity => 13;

        public int ColumnCount => 4;

        public bool MoveCard(int sourceIndex, IZone target, int targetIndex)
        {
            return true;
        }

        public Column GetColumn(int columnIndex)
        {
            return _columns[columnIndex];
        }

        public void DebugInfo()
        {
            for (int i = 0; i < ColumnCount; i++)
            {
                Console.WriteLine($"{nameof(Homecells).ToLower()}[{i}]:{_columns[i]}");
            }
        }

        public IZone Clone()
        {
            Homecells clone = new Homecells(this._game);
            clone.Init();
            for (int i = 0; i < this.ColumnCount; i++)
            {
                clone.GetColumn(i).AddCards(this.GetColumn(i).ToNotation());
            }
            return clone;
        }
    }
}
