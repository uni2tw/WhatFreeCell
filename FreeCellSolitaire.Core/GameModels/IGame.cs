using FreeCellSolitaire.Entities.GameEntities;

namespace FreeCellSolitaire.Core.GameModels;


public interface IGame
{
    int GetExtraMobility();
    Homecells Homecells { get; set; }
    Foundations Foundations { get; set; }
    Tableau Tableau { get; set; }

    void Move(string notation);
    void DebugInfo(int stepNum);
    string GetDebugInfo(int stepNum);
    bool IsCompleted();
    bool EstimateGameover();
    bool EnableAssist { get; set; }
    void TryAssistMove();
    IGame Clone();
}

