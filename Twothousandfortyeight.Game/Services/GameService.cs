namespace Twothousandfortyeight.Game.Services;
internal class GameService : IGameService
{
    private List<Action> _callbacks = new List<Action>();

    public GameGrid Grid { get; init; } = new GameGrid();

    public bool GameStarted
    {
        get => this.Grid.GameStarted; set { }
    }

    public bool GameOver
    {
        get => Grid.GameOver; set { }
    }

    public int Score { get => Grid.Score; set { } }

    public void Reset()
    {
        Grid.InitializeGame();

        ExecuteCallbacks();
    }

    private void ExecuteCallbacks()
    {
        foreach (var c in _callbacks)
        {
            c.Invoke();
        }
    }

    public void Traverse(GameGridTraversalDirection dir)
    {
        Grid.Traverse(dir);

        ExecuteCallbacks();
    }

    public void RegisterOnGridChangedCallback(Action callback)
    {
        _callbacks.Add(callback);
    }
}