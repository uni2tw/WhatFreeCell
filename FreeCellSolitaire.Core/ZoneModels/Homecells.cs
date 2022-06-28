using FreeCellSolitaire.Core.GameModels;

namespace FreeCellSolitaire.Entities.GameEntities
{
    /// <summary>
    /// 右上完成區
    /// </summary>
    public class Homecells : IZone
    {
        private IGame _game;
        /// <summary>
        /// 初始 右上完成區
        /// </summary>
        /// <param name="form"></param>
        public Homecells(IGame game)
        {
            this._game = game;
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
        public int ColumnCapacity
        {
            get
            {
                return 13;
            }
        }


        public Column[] _columns = new Column[4];

        public void Init()
        {
            this._columns[0] = new Column(this,0, ColumnCapacity);
            this._columns[1] = new Column(this,1, ColumnCapacity);
            this._columns[2] = new Column(this,2, ColumnCapacity);
            this._columns[3] = new Column(this,3, ColumnCapacity);
        }


        public bool MoveCard(int sourceIndex, IZone target, int targetIndex)
        {
            return true;
        }
    }
}
