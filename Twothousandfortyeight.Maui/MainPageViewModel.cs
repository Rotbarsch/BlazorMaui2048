using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Twothousandfortyeight.Game;
using Twothousandfortyeight.Game.Services;
using Twothousandfortyeight.Maui.KeyHandler;

namespace Twothousandfortyeight.Maui;

public class MainPageViewModel : INotifyPropertyChanged
{
    private readonly IGameService _gameService;
    private readonly IKeyHandler _keyHandler;
    private int _score = 0;
    private ICommand _onSwipeCommand;
    private ICommand _onClickCommand;
    
    public int Score
    {
        get => _score;
        set
        {
            if (value == _score) return;
            _score = value;
            OnPropertyChanged();
        }
    }

    public ICommand OnSwipeCommand
    {
        get => _onSwipeCommand;
        set
        {
            if (Equals(value, _onSwipeCommand)) return;
            _onSwipeCommand = value;
            OnPropertyChanged();
        }
    }

    public ICommand OnClickCommand
    {
        get => _onClickCommand;
        set
        {
            if (Equals(value, _onClickCommand)) return;
            _onClickCommand = value;
            OnPropertyChanged();
        }
    }

    public MainPageViewModel(IGameService gameService, IKeyHandler keyHandler)
    {
        _gameService = gameService;
        _keyHandler = keyHandler;
        _gameService.Reset();

        OnClickCommand = new Command((e) =>
        {
            _gameService!.Reset();
        });
        
        OnSwipeCommand = new Command((e) =>
        {
            if (Enum.TryParse<GameGridTraversalDirection>(e.ToString(), out var dir))
            {
                _gameService!.Traverse(dir);
            }
        });

        _gameService.RegisterOnGridChangedCallback(() =>
        {
            Score = _gameService.Score;
        });
    }

    public void RegisterKeys(IViewHandler? handler)
    {
        _keyHandler.RegisterGameKeyHandler(handler, Traverse);
    }

    public void Traverse(GameGridTraversalDirection dir)
    {
        _gameService.Traverse(dir);
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}