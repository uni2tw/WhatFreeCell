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
        if (this.Moveable(destColumn) == false)
        {
            return false;
        }

        var cards = this.GetLinkedCards();
        foreach(var card in cards)
        {
            //move
            if (destColumn.AddCards(card))
            {
                Debug.Assert(card.Owner.RemoveCard(card));
                card.SetOwner(destColumn);                
            }
        }        
        return true;
    }

    /// <summary>
    /// 包括自己與自已底下的卡片
    /// </summary>
    /// <returns></returns>
    public List<CardView> GetLinkedCards()
    {
        var result = new List<CardView>(); ;
        for (int i = this.Index(); i < this.Owner.GetCardsCount(); i++)
        {
            result.Add(this.Owner.GetCard(i));
        }        
        return result;
    }

    private int Index()
    {
        for (int i = 0; i < this.Owner.GetCardsCount(); i++)
        {
            if (this.Owner.GetCard(i) == this)
            {
                return i;
            }
        }
        return -1;
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

    public string ToNotation()
    {
        return $"{_card.ToNotation()}";
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
        var srcColumn = this.Owner;
        if (srcColumn.Draggable() == false)
        {
            return false;
        }
        if (destColumn.Droppable(this) == false)
        {
            return false;
        }

        var srcCard = this;

        Debug.Assert(srcCard != null);
        Debug.Assert(srcCard.Owner != null);
        Debug.Assert(srcCard.Owner.Owner != null);
        Debug.Assert(destColumn != null);
        Debug.Assert(destColumn.Owner != null);
        
        IZone destZone = destColumn.Owner;
        IZone srcZone = srcCard.Owner.Owner;

        var destCard = destColumn.GetLastCard();
        return CheckLinkable(destCard, destZone.GetType());
    }

    /// <summary>
    /// 如果這張牌，還被tableu裏的牌需要，就不能被移到homecells
    /// </summary>
    /// <param name="tableau"></param>
    /// <returns></returns>
    public bool NeededByOthers(Tableau tableau)
    {
        bool result = false;
        var srcCard = this;
        for (int i = 0; i < tableau.ColumnCount; i++)
        {
            var col = tableau.GetColumn(i);
            for (int j = 0; j < col.GetCardsCount(); j++)
            {
                var destCard = col.GetCard(j);
                if (destCard.Number != 1 && destCard.CheckLinkable(srcCard, typeof(Tableau)))
                {
                    result = true;
                }
            }
        }
        return result;
    }

    /// <summary>
    /// 牌移動時，判斷是否可以移到目標牌的下方
    /// </summary>
    /// <param name="destCard"></param>
    /// <param name="zoneType"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    private bool CheckLinkable(CardView destCard, Type zoneType)
    {
        var srcCard = this;

        Debug.Assert(srcCard != null);
        Debug.Assert(srcCard.Owner != null);
        Debug.Assert(srcCard.Owner.Owner != null);

        if (zoneType == typeof(Tableau))
        {
            if (destCard == null) return true;
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
        else if (zoneType == typeof(Homecells))
        {
            if (destCard == null)
            {
                return srcCard.Number == 1;
            }
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
        else if (zoneType == typeof(Foundations))
        {
            if (destCard == null) return true;
            return false;
        }
        throw new Exception($"zoneType unspecified fail");
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
