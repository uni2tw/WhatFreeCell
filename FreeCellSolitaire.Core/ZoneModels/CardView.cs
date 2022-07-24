using FreeCellSolitaire.Core.CardModels;
using FreeCellSolitaire.Entities.GameEntities;
using System.Diagnostics;

namespace FreeCellSolitaire.Core.GameModels;

/// <summary>
/// 一張牌
/// </summary>
public class CardView
{
    private Card _card;
    public CardView(Column owner, Card card)
    {
        this.Owner = owner;
        _card = card;
    }
    public bool IsSelected { get; set; }
    public Column Owner { get; private set; }
    public void SetOwner(Column column)
    {
        this.Owner = column;
    }
    public bool Move(Column destColumn)
    {
        var srcColumn = this.Owner;
        if (srcColumn.Draggable() == false)
        {
            return false;
        }
        if (destColumn.Droppable(this) == false)
        {
            return false;
        }    
        if (this.Moveable(destColumn) == false)
        {
            return false;
        }

        //move
        if (destColumn.AddCards(this))
        {
            Debug.Assert(this.Owner.RemoveCard(this));
            this.SetOwner(destColumn);
            return true;
        }
        
        return false;
    }


    public int Number
    {
        get
        {
            return _card.Number;
        }
    }

    public CardSuit Suit
    {
        get
        {
            return _card.Suit;
        }
    }

    public override string ToString()
    {
        return $"{_card}";
    }



    /// <summary>
    /// 判斷花色等
    /// Foundations 沒有限定
    /// Homecells 相同花色 依小到大
    /// Tableau 黑紅交雜 依小到到
    /// </summary>
    /// <param name="destColumn"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public bool Moveable(Column destColumn)
    {
        var srcCard = this;

        Debug.Assert(srcCard != null);
        Debug.Assert(srcCard.Owner != null);
        Debug.Assert(srcCard.Owner.Owner != null);
        Debug.Assert(destColumn != null);
        Debug.Assert(destColumn.Owner != null);
        
        IZone destZone = destColumn.Owner;
        IZone srcZone = srcCard.Owner.Owner;

        var destCard = destColumn.GetLastCard();        
        if (destCard == null) return true;

        if (destZone.GetType() == typeof(Tableau))
        {
            if (srcCard.IsBlack() && destCard.IsRed())
            {
                if (srcCard.Number - destCard.Number == -1)
                {
                    return true;
                }
            }
            if (srcCard.IsRed() && destCard.IsBlack())
            {
                if (srcCard.Number - destCard.Number == -1)
                {
                    return true;
                }
            }
            return false;
        }
        else if (destZone.GetType() == typeof(Homecells))
        {
            //相同花色，點數大的可以放置在點數小的後面
            if (srcCard.Suit == destCard.Suit)
            {
                if (srcCard.Number - destCard.Number == 1)
                {
                    return true;
                }
            }
            return false;
        }
        else if (destZone.GetType() == typeof(Foundations))
        {
            return true;
        }

        throw new Exception($"Zone unspecified, move fail");
    }    

    public bool IsRed()
    {
        if (this._card.Suit == CardSuit.Heart || this._card.Suit == CardSuit.Diamond )
        {
            return true;             
        }
        return false;
    }
    public bool IsBlack()
    {
        if (this._card.Suit == CardSuit.Spade || this._card.Suit == CardSuit.Club)
        {
            return true;
        }
        return false;
    }
}
