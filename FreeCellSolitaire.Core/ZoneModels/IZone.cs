namespace FreeCellSolitaire.Core.GameModels
{
    public interface IZone
    {
        bool CanInternalSwap { get; }
        bool CanMoveIn { get; }
        bool CanMoveOut { get; }
        int ColumnCapacity { get; }

        bool MoveCard(int sourceIndex, IZone target, int targetIndex);

    }
}
