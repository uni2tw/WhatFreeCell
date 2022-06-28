using FreeCellSolitaire.Core.CardModels;

namespace FreeCellSolitaire.Core.GameModels;

/// <summary>
/// 一張牌
/// </summary>
public class CardView
{
    private Card _card;
    public CardView(Column owner, Card card)
    {
        this._owner = owner;
        _card = card;
    }
    public bool IsSelected { get; set; }
    public Column _owner { get; set; }

    public bool Move(Column dest)
    {
        return false;
    }

    public override string ToString()
    {
        return $"{_card}";
    }
}
