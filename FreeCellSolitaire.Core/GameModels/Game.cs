using FreeCellSolitaire.Entities.GameEntities;
using System.Diagnostics;
using System.Text;
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
    public void TryAssistMove()
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

    public string GetDebugInfo(int stepNum)
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine($"=== {stepNum} ===");
        if (this.Tableau != null)
        {
            sb.Append(this.Tableau.GetDebugInfo(false));
        }
        if (this.Foundations != null)
        {
            sb.Append(this.Foundations.GetDebugInfo(false));
        }
        if (this.Homecells != null)
        {
            sb.Append(this.Homecells.GetDebugInfo());
        }
        return sb.ToString();
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
        var clone = new Game { EnableAssist = this.EnableAssist };
        clone.Tableau = this.Tableau.Clone() as Tableau;
        clone.Homecells = this.Homecells.Clone() as Homecells;
        clone.Foundations = this.Foundations.Clone() as Foundations;
        return clone;
    }

    public bool EstimateGameover()
    {
        bool gameove = false;
        Queue<IGame> queueItems = new Queue<IGame>();
        HashSet<IGame> samples = new HashSet<IGame>();
        queueItems.Enqueue(this.Clone());
        int depth = 0;
        while (queueItems.Count > 0 || depth <= 2)
        {
            var data = GetPossibleSituations(queueItems.Dequeue(), ref depth);

            foreach (var datum in data)
            {                
                if (samples.Contains(datum) == false)
                {
                    datum.DebugInfo(2 * 10 + data.IndexOf(datum) + 1);
                    queueItems.Enqueue(datum);
                    samples.Add(datum);
                }
            }
        };
        gameove = samples.Count <= 2;
        return gameove;
    }

    public List<IGame> GetPossibleSituations(IGame game, ref int depth)
    {
        depth++;
        List<IGame> samples = new List<IGame>();        
        for (int i = 0; i < game.Tableau.ColumnCount + game.Foundations.ColumnCount; i++)
        {
            for (int j = 0; j < game.Tableau.ColumnCount + game.Foundations.ColumnCount; j++)
            {
                var clone = game.Clone();

                Column srcColumn;
                if (i < clone.Tableau.ColumnCount)
                {
                    srcColumn = clone.Tableau.GetColumn(i);
                }
                else
                {
                    srcColumn = clone.Foundations.GetColumn(i - clone.Tableau.ColumnCount);
                }

                var srcCard = srcColumn.GetLastCard();
                if (srcCard == null)
                {
                    continue;
                }
                Column destColumn;
                if (j < clone.Tableau.ColumnCount)
                {
                    destColumn = clone.Tableau.GetColumn(j);
                }
                else
                {
                    destColumn = clone.Foundations.GetColumn(j - clone.Tableau.ColumnCount);
                }
                
                if (srcCard.Move(destColumn))
                {
                    if (EnableAssist)
                    {
                        clone.TryAssistMove();
                    }
                    samples.Add(clone);
                    continue;
                }
            }
        }
        return samples;        
    }

    public override bool Equals(object? obj)
    {
        if (obj == null)
        {
            return false;
        }
        if (this == obj)
        {
            return true;
        }
        var castObj = obj as IGame;
        if (castObj == null)
        {
            return false;
        }
        return castObj.GetDebugInfo(0) == this.GetDebugInfo(0);
    }

    public override int GetHashCode()
    {
        return GetDebugInfo(0).GetHashCode();
    }

}

