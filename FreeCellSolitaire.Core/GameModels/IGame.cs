using FreeCellSolitaire.Entities.GameEntities;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace FreeCellSolitaire.Core.GameModels;


public interface IGame
{
    int GetExtraMobility();
    Homecells Homecells { get; set; }
    Foundations Foundations { get; set; }
    Tableau Tableau { get; set; }

    void Move(string notation);
    void DebugInfo(int stepNum);
    bool IsCompleted();
}

public class Game : IGame
{    
    public Homecells Homecells { get; set; }

    public Foundations Foundations { get; set; }

    public Tableau Tableau { get; set; }

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
}

