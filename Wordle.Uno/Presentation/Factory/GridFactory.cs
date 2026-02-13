namespace Wordle.Uno.Presentation.Factory;

public static class GridFactory
{
    private static readonly Random Random = new();

    public static Grid CreateDefaultGrid()
    {
        return new Grid()
        {
            Margin = new Thickness(2),
            IsTabStop = false,
            Background = GetBackgroundBrush(true),
        };
    }

    private static Brush GetBackgroundBrush(bool useDebugBrush)
    {
        return !useDebugBrush ? new SolidColorBrush(Colors.Transparent) : GetRandomBrush();
    }

    private static Brush GetRandomBrush()
    {
        byte r = (byte) Random.Next(256);
        byte g = (byte) Random.Next(256);
        byte b = (byte) Random.Next(256);

        return new SolidColorBrush(Color.FromArgb(255, r, g, b));
    }
}
