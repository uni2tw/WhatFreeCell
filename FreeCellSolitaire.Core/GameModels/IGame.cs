using FreeCellSolitaire.Core.CardModels;
using FreeCellSolitaire.Entities.GameEntities;

namespace FreeCellSolitaire.Core.GameModels;


public interface IGame
{
    int GetExtraMobility();
    Homecells Homecells { get; set; }
    Foundations Foundations { get; set; }
    Tableau Tableau { get; set; }

    Deck Deck { get; }
    bool Move(string notation);
    void DebugInfo(string stepNum = "", bool enabled = true);
    void DebugInfo(int stepNum, bool enabled = true);
    string GetDebugInfo(string stepNum = "", bool enabled = true);
    bool IsCompleted();
    bool IsPlaying();
    GameStatus EstimateGameover(bool debug = false);
    bool EnableAssist { get; set; }
    void TryAssistMove();
    IGame Clone();
}

