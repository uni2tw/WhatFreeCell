using FreeCellSolitaire.Entities.GameEntities;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace FreeCellSolitaire.Core.GameModels;

public class Game : IGame
{        
    public Homecells Homecells { get; set; }

    public Foundations Foundations { get; set; }

    public Tableau Tableau { get; set; }

    public bool EnableAssist { get; set; }

    public Game()
    {
    }

    public int GetExtraMobility()
    {
        int result = 0;
        for (int i = 0; i < Tableau.ColumnCount; i++)
        {
            if (Tableau.GetColumn(i).Empty())
            {
                result++;
            }
        }
        for (int i = 0; i < Foundations.ColumnCount; i++)
        {
            if (Foundations.GetColumn(i).Empty())
            {
                result++;
            }
        }
        return result;
    }

    Regex regNotation = new Regex(@"([ft])(\d{1,2})([fth])(\d{1,2})", RegexOptions.Singleline | RegexOptions.Compiled);
    public void Move(string notation)
    {
        Match match = regNotation.Match(notation);

        string srcZone = match.Groups[1].Value;
        string destZone = match.Groups[3].Value;
        int srcColumn = int.Parse(match.Groups[2].Value);
        int destColumn = int.Parse(match.Groups[4].Value);

        CardView card = null;
        if (srcZone == "t")
        {
            card = this.Tableau.GetColumn(srcColumn).GetLastCard();
        } 
        else if (srcZone == "f")
        {
            card = this.Foundations.GetColumn(srcColumn).GetLastCard();
        }
        Debug.Assert(card != null);
        if (destZone == "t")
        {
            Debug.Assert(card.Move(Tableau.GetColumn(destColumn)));
        }
        else if (destZone == "f")
        {
            Debug.Assert(card.Move(Foundations.GetColumn(destColumn)));
        }
        else if (destZone == "h")
        {
            Debug.Assert(card.Move(Homecells.GetColumn(destColumn)));
        }

        if (EnableAssist)
        {
            TryAssistMove();
        }
    }
    private void TryAssistMove()
    {
        bool anything;
        do
        {
            anything = false;
     

            Func<CardView, bool> TryMoveToHomecells = delegate (CardView theCard)
            {
                for (int i = 0; i < this.Homecells.ColumnCount; i++)
                {
                    if (theCard.Moveable(Homecells.GetColumn(i)) && theCard.NeededByOthers(this.Tableau) == false)
                    {
                        if (theCard.Move(Homecells.GetColumn(i)))
                        {
                            return true;
                        }
                    }
                }
                return false;
            };

            for (int i = 0; i < this.Foundations.ColumnCount; i++)
            {
                var srcCard = this.Foundations.GetColumn(i).GetLastCard();
                if (srcCard != null)
                {
                    if (TryMoveToHomecells(srcCard))
                    {
                        anything = true;
                        break;
                    }
                }
            }
            if (anything == false)
            {
                for (int i = 0; i < this.Tableau.ColumnCount; i++)
                {
                    var srcCard = this.Tableau.GetColumn(i).GetLastCard();
                    if (srcCard != null)
                    {
                        if (TryMoveToHomecells(srcCard))
                        {
                            anything = true;
                            break;
                        }
                    }
                }
            }
        } while (anything);
    }

    public void DebugInfo(int stepNum)
    {
        Console.WriteLine($"=== {stepNum} ===");
        this.Tableau?.DebugInfo(false);
        this.Foundations?.DebugInfo(false);
        this.Homecells?.DebugInfo();
    }

    public bool IsCompleted()
    {
        if (Homecells.GetColumn(0).GetCardsCount() +
            Homecells.GetColumn(1).GetCardsCount() +
            Homecells.GetColumn(2).GetCardsCount() +
            Homecells.GetColumn(3).GetCardsCount() == 52)
        {
            return true;
        }
        return false;
    }

    public IGame Clone()
    {
        return new Game
        {
            EnableAssist = this.EnableAssist,
            Tableau = this.Tableau.Clone() as Tableau,
            Foundations = this.Foundations.Clone() as Foundations,
            Homecells = this.Homecells.Clone() as Homecells
        };
    }

    public bool IsGameover()
    {
        return false;
    }

}

