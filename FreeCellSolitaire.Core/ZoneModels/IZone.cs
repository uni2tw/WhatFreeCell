namespace FreeCellSolitaire.Core.GameModels
{
    public interface IZone
    {
        IGame Game { get; }
        bool CanInternalSwap { get; }
        bool CanMoveIn { get; }
        bool CanMoveOut { get; }
        int ColumnCapacity { get; }
        int ColumnCount { get; }

        string NotationCode { get; }

        bool MoveCard(int sourceIndex, IZone target, int targetIndex);

        Column GetColumn(int columnIndex);
        public IZone Clone();

    }
}
