using Wordle.Uno.Abstraction;
using Wordle.Uno.Presentation.Region;

namespace Wordle.Uno.NavigationRegion;

public sealed class HistoryPageRegionDefinition : IPageRegion
{
    public string DisplayName => "History";

    public IconElement Icon => new SymbolIcon(Symbol.World);

    public UIElement CreateControl(IServiceProvider services)
    {
        Console.WriteLine($"Changing page to: {nameof(HistoryPageRegion)}");
        // Use DI to build the region
        return ActivatorUtilities.CreateInstance<HistoryPageRegion>(services);
    }
}
