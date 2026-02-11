namespace Wordle.Uno.Presentation.Factory;

public static class GridFactory
{
    public static Grid CreateDefaultGrid()
    {
        return new Grid()
        {
            Margin = new Thickness(2),
            IsTabStop = false,
        };
    }
}
