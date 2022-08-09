using FreeCellSolitaire.Entities.GameEntities;

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
            if (game != null)
            {
                this._game.Foundations = this;
            }
            Init();
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

        public void DebugInfo(bool timeStamp = true)
        {
            if (timeStamp)
            {
                Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            }
            for (int i = 0; i < ColumnCount; i++)
            {
                Console.WriteLine($"{nameof(Foundations).ToLower()}[{i}]:{_columns[i]}");
            }
        }

        public IZone Clone()
        {
            Foundations clone = new Foundations(this._game);
            clone.Init();
            for (int i = 0; i < this.ColumnCount; i++)
            {
                clone.GetColumn(i).AddCards(this.GetColumn(i).ToNotation());
            }
            return clone;
        }
    }
}
