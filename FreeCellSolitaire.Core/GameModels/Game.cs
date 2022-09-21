using FreeCellSolitaire.Core.CardModels;
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
        Tracks = new TrackList();
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

    static Regex regNotation = new Regex(@"([ft])(\d{1,2})([fth])(\d{1,2})", RegexOptions.Singleline | RegexOptions.Compiled);
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

        Tracks.Add(notation);

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

    public void DebugInfo(int stepNum, bool enabled = true)
    {
        Console.Write(GetDebugInfo(stepNum.ToString(), enabled));
    }
    public void DebugInfo(string stepNum = "", bool enabled = true)
    {
        Console.Write(GetDebugInfo(stepNum, enabled));
    }

    public TrackList Tracks { get; set; }

    public string GetDebugInfo(string stepNum = "", bool enabled = true, bool trackInfo = true)
    {
        if (enabled == false)
        {
            return string.Empty;
        }
        StringBuilder sb = new StringBuilder();
        if (string.IsNullOrEmpty(stepNum) == false)
        {
            sb.AppendLine($"=== {stepNum} ===");
        }
        if (trackInfo)
        {
            sb.AppendLine($"-- tracks: {string.Join(',', Tracks)}");
        }
        
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

    public bool IsPlaying()
    {
        int unfinish = 0;
        for(int i = 0; i < Tableau.ColumnCount; i++)
        {
            unfinish += Tableau.GetColumn(i).GetCardsCount();
        }
        return unfinish > 0;
    }

    public bool IsCompleted()
    {
        int todoCount = 0;
        for(int i = 0; i < Foundations.ColumnCount; i++)
        {
            todoCount += Foundations.GetColumn(i).GetCardsCount();
        }
        for (int i = 0; i < Tableau.ColumnCount; i++)
        {
            todoCount += Tableau.GetColumn(i).GetCardsCount();
        }
        return todoCount == 0;
    }

    public IGame Clone()
    {
        var clone = new Game { EnableAssist = this.EnableAssist };
        clone.Tableau = this.Tableau.Clone() as Tableau;
        clone.Homecells = this.Homecells.Clone() as Homecells;
        clone.Foundations = this.Foundations.Clone() as Foundations;
        clone.Tracks = new TrackList(this.Tracks);
        return clone;
    }

    public GameStatus EstimateGameover(bool debug = false)
    {
        if (IsCompleted())
        {
            return GameStatus.Completed;
        }
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
                    queueItems.Enqueue(datum);
                    samples.Add(datum);
                    datum.DebugInfo($"s-{samples.Count}", debug);
                }
            }

            if (samples.Count > 2)
            {
                return GameStatus.Playable;
            }
        };

        if (samples.Count == 0)
        {
            return GameStatus.DeadEnd;
        }
        else if (samples.Count <= 2)
        {
            return GameStatus.Checkmate;
        }
        return GameStatus.Playable;
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

                string notation;
                if (srcCard.Move(destColumn, out notation))
                {                                    
                    if (EnableAssist)
                    {
                        clone.TryAssistMove();
                    }
                    Tracks.Add(notation);
                    samples.Add(clone);
                    continue;
                }
            }
        }
        return samples;        
    }

    public bool FindTheEnd(out int total, out List<TrackList> completions)
    {
        Queue<IGame> queueItems = new Queue<IGame>();
        HashSet<IGame> samples = new HashSet<IGame>();
        queueItems.Enqueue(this.Clone());
        int depth = 0;
        while (queueItems.Count > 0)
        {
            var data = GetPossibleSituations2(queueItems.Dequeue(), ref depth);

            foreach (var datum in data)
            {
                if (samples.Contains(datum) == false)
                {
                    queueItems.Enqueue(datum);
                    samples.Add(datum);
                    //Console.WriteLine(datum.GetDebugInfo($"s-{samples.Count}") + $" - completed is {datum.IsCompleted()}");
                }
            }
        };        
        total = samples.Count;
        completions = new List<TrackList>();
        foreach (var sample in samples)
        {
            if (sample.IsCompleted())
            {
                completions.Add(sample.Tracks);
            }
        }
        return completions.Count > 0;
    }

    public List<IGame> GetPossibleSituations2(IGame game, ref int depth)
    {
        depth++;
        List<IGame> samples = new List<IGame>();
        for (int src = 0; src < game.Tableau.ColumnCount + game.Foundations.ColumnCount; src++)
        {
            for (int dest = 0; dest < game.Tableau.ColumnCount + game.Foundations.ColumnCount+game.Homecells.ColumnCount; dest++)
            {
                var clone = game.Clone();

                Column srcColumn;
                if (src < clone.Tableau.ColumnCount)
                {
                    srcColumn = clone.Tableau.GetColumn(src);
                }
                else
                {
                    srcColumn = clone.Foundations.GetColumn(src - clone.Tableau.ColumnCount);
                }

                var srcCard = srcColumn.GetLastCard();
                if (srcCard == null)
                {
                    continue;
                }
                Column destColumn;
                if (dest < clone.Tableau.ColumnCount)
                {
                    destColumn = clone.Tableau.GetColumn(dest);
                }
                else if (dest < game.Tableau.ColumnCount + game.Foundations.ColumnCount)
                {
                    destColumn = clone.Foundations.GetColumn(dest - clone.Tableau.ColumnCount);
                }
                else
                {
                    destColumn = clone.Homecells.GetColumn(dest - clone.Tableau.ColumnCount - game.Foundations.ColumnCount);
                }
                string notation;
                if (srcCard.Move(destColumn, out notation))
                {
                    if (EnableAssist)
                    {
                        clone.TryAssistMove();
                    }
                    clone.Tracks.Add(notation);
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
        return castObj.GetDebugInfo("", true, false) == this.GetDebugInfo("", true, false);        
    }

    public override int GetHashCode()
    {
        return GetDebugInfo("", true, false).GetHashCode();
        
    }

}

public enum GameStatus
{
    Playable, Completed, Checkmate, DeadEnd
}

