using Windows.System;
using Microsoft.UI.Xaml;
using Twothousandfortyeight.Game;
using KeyboardAccelerator = Microsoft.UI.Xaml.Input.KeyboardAccelerator;

// ReSharper disable once CheckNamespace
namespace Twothousandfortyeight.Maui.KeyHandler;

public class KeyHandler : IKeyHandler
{
    public void RegisterGameKeyHandler(IElementHandler? handler, Action<GameGridTraversalDirection> traversalAction)
    {
        var accelerators = (handler.PlatformView as UIElement).KeyboardAccelerators;

        foreach (var key in new[] {
                     VirtualKey.Up, VirtualKey.Down, VirtualKey.Left, VirtualKey.Right,
                     VirtualKey.NumberPad8, VirtualKey.NumberPad6, VirtualKey.NumberPad4, VirtualKey.NumberPad2,
                 })
        {
            var acc = new KeyboardAccelerator
            {
                Key = key,
            };
            acc.Invoked += ((_, args) =>
            {
                switch (args.KeyboardAccelerator.Key)
                {
                    case VirtualKey.NumberPad8:
                    case VirtualKey.Up:
                        traversalAction.Invoke(GameGridTraversalDirection.UP);
                        break;
                    case VirtualKey.NumberPad2:
                    case VirtualKey.Down:
                        traversalAction.Invoke(GameGridTraversalDirection.DOWN);
                        break;
                    case VirtualKey.NumberPad4:
                    case VirtualKey.Left:
                        traversalAction.Invoke(GameGridTraversalDirection.LEFT);
                        break;
                    case VirtualKey.NumberPad6:
                    case VirtualKey.Right:
                        traversalAction.Invoke(GameGridTraversalDirection.RIGHT);
                        break;
                }
                args.Handled = true;
            });

            accelerators.Add(acc);
        }
    }
}