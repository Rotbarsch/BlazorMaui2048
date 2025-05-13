using Twothousandfortyeight.Game;

// ReSharper disable once CheckNamespace
namespace Twothousandfortyeight.Maui.KeyHandler;

/// <summary>
/// MAUI doesn't offer a cross-platform approach to handling Keyboard Events as of now. This is a PoC implementation.
/// </summary>
public interface IKeyHandler
{
    void RegisterGameKeyHandler(IViewHandler? handler, Action<GameGridTraversalDirection> traversalAction);
}