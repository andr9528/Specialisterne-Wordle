using Wordle.Uno.Abstraction;
using Wordle.Uno.Presentation.Region;

namespace Wordle.Uno.NavigationRegion;

public sealed class GamePageRegionDefinition : IPageRegion
{
    public string DisplayName => "Game";

    public IconElement Icon => new SymbolIcon(Symbol.Play);

    public UIElement CreateControl(IServiceProvider services)
    {
        Console.WriteLine($"Changing page to: {nameof(GamePageRegion)}");
        // Use DI to build the region
        return ActivatorUtilities.CreateInstance<GamePageRegion>(services);
    }
}
