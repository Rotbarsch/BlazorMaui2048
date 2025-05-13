using System.Windows.Input;
using Twothousandfortyeight.Game;
using Twothousandfortyeight.Game.Services;
using Twothousandfortyeight.Maui.KeyHandler;

namespace Twothousandfortyeight.Maui;

public partial class MainPage
{
    private readonly IGameService _gameService;
    private readonly IKeyHandler _keyHandler;
    private int _score = 0;
    private ICommand _onSwipeCommand;
    private ICommand _onClickCommand;

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

    protected override void OnHandlerChanged()
    {
        _keyHandler.RegisterGameKeyHandler(this.Handler, (dir) =>
        {
            _gameService!.Traverse(dir);
        });
    }

    public MainPage(IGameService service, IKeyHandler keyHandler)
    {
        InitializeComponent();
        BindingContext = this;
        
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

        _gameService = service;
        _keyHandler = keyHandler;
        _gameService.RegisterOnGridChangedCallback(() =>
        {
            Score = _gameService.Score;
        });
    }


    public int Score
    {
        get => _score;
        set
        {
            if (value == _score) return;
            _score = value;
            OnPropertyChanged();
            OnPropertyChanged();
        }
    }
}