namespace Twothousandfortyeight.Game.Services;

public interface IGameService
{
    GameGrid Grid { get; init; }
    bool GameStarted { get; set; }
    bool GameOver { get; set; }
    int Score { get; set; }
    void Reset();
    void Traverse(GameGridTraversalDirection dir);
    void RegisterOnGridChangedCallback(Action callback);
}