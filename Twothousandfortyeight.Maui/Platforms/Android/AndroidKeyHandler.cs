using Android.Views;
using Twothousandfortyeight.Game;
using View = Android.Views.View;

// ReSharper disable once CheckNamespace
namespace Twothousandfortyeight.Maui.KeyHandler;

/// <summary>
/// Compare <see cref="MainActivity"/> to see where the event is raised.
/// </summary>
public class KeyHandler : IKeyHandler
{
    private Action<GameGridTraversalDirection> _keyAction;
    static event EventHandler KeyUp;

    public static void InvokeKeyUpEvent(Keycode keyCode, KeyEvent? keyEvent)
    {
        KeyUp.Invoke(null, new View.KeyEventArgs(true, keyCode, keyEvent));
    }

    public void RegisterGameKeyHandler(IViewHandler? handler, Action<GameGridTraversalDirection> traversalAction)
    {
        KeyUp += HandleKeyUp;
        _keyAction = traversalAction;
    }

    private void HandleKeyUp(object? sender, EventArgs e)
    {
        switch ((e as View.KeyEventArgs)?.KeyCode)
        {
            case Keycode.DpadUp:
                _keyAction?.Invoke(GameGridTraversalDirection.UP);
                break;
            case Keycode.DpadLeft:
                _keyAction?.Invoke(GameGridTraversalDirection.LEFT);
                break;
            case Keycode.DpadRight:
                _keyAction?.Invoke(GameGridTraversalDirection.RIGHT);
                break;
            case Keycode.DpadDown:
                _keyAction?.Invoke(GameGridTraversalDirection.DOWN);
                break;
        }
    }
}