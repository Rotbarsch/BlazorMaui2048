using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using Microsoft.Extensions.DependencyInjection;
using Twothousandfortyeight.Game;
using Twothousandfortyeight.Game.Services;
using Window = System.Windows.Window;

namespace Twothousandfortyeight.Wpf;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window, INotifyPropertyChanged
{
    private readonly IGameService _gameService;
    private int _score = 0;

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

    public MainWindow()
    {
        InitializeComponent();

        var serviceCollection = new ServiceCollection();
        serviceCollection.AddWpfBlazorWebView();
        serviceCollection.AddBlazorWebViewDeveloperTools();
        serviceCollection.Add2048GameServices();

        var sp = serviceCollection.BuildServiceProvider();

        _gameService = sp.GetService<IGameService>()!;
        _gameService.Reset();
        _gameService.RegisterOnGridChangedCallback(() =>
        {
            Score = _gameService.Score;
        });

        Resources.Add("services", sp);
    }

    private void MainWindow_OnKeyUp(object sender, KeyEventArgs e)
    {
        switch (e.Key)
        {
            case Key.Up:
            case Key.NumPad8:
                _gameService.Traverse(GameGridTraversalDirection.UP);
                break;
            case Key.Left:
            case Key.NumPad4:
                _gameService.Traverse(GameGridTraversalDirection.LEFT);
                break;
            case Key.Right:
            case Key.NumPad6:
                _gameService.Traverse(GameGridTraversalDirection.RIGHT);
                break;
            case Key.Down:
            case Key.NumPad2:
                _gameService.Traverse(GameGridTraversalDirection.DOWN);
                break;
            case Key.Space:
                _gameService.Reset();
                break;
            case Key.Escape:
                this.Close();
                break;
        }


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

    private void OnExitClicked(object sender, RoutedEventArgs e)
    {
        this.Close();
    }

    private void OnResetClicked(object sender, RoutedEventArgs e)
    {
        _gameService.Reset();
    }
}