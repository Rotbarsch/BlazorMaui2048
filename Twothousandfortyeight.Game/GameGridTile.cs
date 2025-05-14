using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Twothousandfortyeight.Game;

public class GameGridTile : INotifyPropertyChanged
{
    public int InternalValue { get; set; } = 0;
    public int ActualValue => InternalValue == 0 ? 0 : (int)Math.Pow(2, InternalValue);

    public int Row { get; internal set; } = 0;
    public int Column { get; internal set; } = 0;

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

    internal void Increment()
    {
        InternalValue++;
    }
}