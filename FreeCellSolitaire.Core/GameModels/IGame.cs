namespace FreeCellSolitaire.Core.GameModels;

public interface IGame
{
    void InitScreen();
    void Reset();
    void Start();
    void InitEvents();
    bool IsPlaying { get; }
}



