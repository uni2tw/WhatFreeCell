namespace FreeCellSolitaire.Core.GameModels
{
    /// <summary>
    /// 左上暫存區
    /// </summary>
    public class Foundations : IZone
    {
        IGame _game;
        public Foundations(IGame game)
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
                return 1;
            }
        }

        public Column[] _columns = new Column[4];

        public void Init()
        {
            _columns[0] = new Column(this, 0,ColumnCapacity);
            _columns[1] = new Column(this, 1,ColumnCapacity);
            _columns[2] = new Column(this, 2,ColumnCapacity);
            _columns[3] = new Column(this, 3,ColumnCapacity);
        }


        public bool MoveCard(int sourceIndex, IZone target, int targetIndex)
        {
            return true;
        }
    }
}
