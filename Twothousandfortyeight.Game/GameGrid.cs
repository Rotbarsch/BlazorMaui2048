using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Twothousandfortyeight.Game;

public class GameGrid : INotifyPropertyChanged
{
    public const int GridSize = 4;
    public List<GameGridTile> Items { get; private set; } = new List<GameGridTile>();

    public int TurnNumber { get; private set; }
    public bool GameStarted { get; private set; } = false;
    public bool GameOver { get; private set; } = false;
    public int Score => Items.Where(x=>x.InternalValue>1).Sum(x => x.ActualValue);

    public void InitializeGame()
    {
        TurnNumber = 0;
        ClearField();
        SpawnTile();
        SpawnTile();
        GameStarted = true;
    }

    private GameGridTile GetTileAtCoordinate(int column, int row)
    {
        return Items.Single(x => x.Column == column && x.Row == row);
    }

    internal void Traverse(GameGridTraversalDirection dir)
    {
        if (TraverseInner(dir))
        {
            TurnNumber++;
            SpawnTile();
        }
        
    }
    
    private bool TraverseInner(GameGridTraversalDirection dir)
    {
        var movedSomething = false;
        var setTiles = Items.Where(x => x.InternalValue > 0).ToList();
        Func<GameGridTile, GameGridTile> neighborFunc;
        Func<GameGridTile, bool> cancelFunc;

        switch (dir)
        {
            case GameGridTraversalDirection.LEFT:
                neighborFunc = tile => GetTileAtCoordinate(tile.Column - 1, tile.Row);
                cancelFunc = tile => tile.Column is 0;
                break;
            case GameGridTraversalDirection.RIGHT:
                neighborFunc = tile => GetTileAtCoordinate(tile.Column + 1, tile.Row);
                cancelFunc = tile => tile.Column is GridSize - 1;
                break;
            case GameGridTraversalDirection.UP:
                neighborFunc = tile => GetTileAtCoordinate(tile.Column, tile.Row - 1);
                cancelFunc = tile => tile.Row is 0;
                break;
            case GameGridTraversalDirection.DOWN:
                neighborFunc = tile => GetTileAtCoordinate(tile.Column, tile.Row + 1);
                cancelFunc = tile => tile.Row is GridSize - 1;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(dir), dir, null);
        }

        foreach (var tile in setTiles)
        {
            if (cancelFunc.Invoke(tile)) continue;
            
            var neighboringTile = neighborFunc.Invoke(tile);

            // Case empty neighbor
            if (neighboringTile.InternalValue == 0)
            {
                neighboringTile.InternalValue = tile.InternalValue;
                tile.InternalValue = 0;
                movedSomething = true;
            }
            // Case fusable
            else if (neighboringTile.InternalValue == tile.InternalValue)
            {
                neighboringTile.Increment();
                tile.InternalValue = 0;
                movedSomething = true;
            }
            // Case blocked
            // do nothing
        }

        if (movedSomething)
        {
            TraverseInner(dir);
        }

        return movedSomething;
    }

    private void ClearField()
    {
        Items.Clear();
        for (int i = 0; i < GridSize; i++)
        {
            for (int j = 0; j < GridSize; j++)
            {
                Items.Add(new GameGridTile
                {
                    Row = i,
                    Column = j,
                });
            }
        }
    }

    private void SpawnTile()
    {
        var nullTiles = Items.Where(x => x.InternalValue == 0).ToArray();
        if (!nullTiles.Any()) return;
        var randIndex = Random.Shared.Next(0, nullTiles.Count());
        nullTiles[randIndex].Increment();
    }


    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}