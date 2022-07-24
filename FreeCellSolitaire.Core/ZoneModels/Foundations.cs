namespace FreeCellSolitaire.Core.GameModels
{
    /// <summary>
    /// 左上暫存區
    /// </summary>
    public class Foundations : IZone
    {
        IGame _game;
        Column[] _columns;
        public Foundations(IGame game)
        {
            this._game = game;
            Init();
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
        public int ColumnCapacity => 1;

        public int ColumnCount => 4;

        public bool MoveCard(int sourceIndex, IZone target, int targetIndex)
        {
            return true;
        }

        public Column GetColumn(int columnIndex)
        {
            return _columns[columnIndex];
        }
    }
}
