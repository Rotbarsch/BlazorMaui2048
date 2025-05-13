using Windows.System;
using Microsoft.UI.Xaml;
using Twothousandfortyeight.Game;

// ReSharper disable once CheckNamespace
namespace Twothousandfortyeight.Maui.KeyHandler;

public class KeyHandler : IKeyHandler
{
    public void RegisterGameKeyHandler(IViewHandler? handler, Action<GameGridTraversalDirection> traversalAction)
    {
        if (handler?.PlatformView is UIElement nativeView)
        {
            nativeView.KeyUp += (sender, args) =>
            {
                switch (args.Key)
                {
                    case VirtualKey.Up:
                        traversalAction.Invoke(GameGridTraversalDirection.UP);
                        break;
                    case VirtualKey.Down:
                        traversalAction.Invoke(GameGridTraversalDirection.DOWN);
                        break;
                    case VirtualKey.Left:
                        traversalAction.Invoke(GameGridTraversalDirection.LEFT);
                        break;
                    case VirtualKey.Right:
                        traversalAction.Invoke(GameGridTraversalDirection.RIGHT);
                        break;
                }

                args.Handled = true;
            };
        }
    }
}