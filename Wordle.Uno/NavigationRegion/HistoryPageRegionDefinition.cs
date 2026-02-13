using Wordle.Uno.Abstraction;
using Wordle.Uno.Presentation.Region;

namespace Wordle.Uno.NavigationRegion;

public sealed class HistoryPageRegionDefinition : IPageRegion
{
    private readonly ILogger<HistoryPageRegionDefinition> logger;

    public HistoryPageRegionDefinition(ILogger<HistoryPageRegionDefinition> logger)
    {
        this.logger = logger;
    }

    public string DisplayName => "History";

    public IconElement Icon => new SymbolIcon(Symbol.World);

    public UIElement CreateControl(IServiceProvider services)
    {
        logger.LogInformation($"Changing page to: {nameof(HistoryPageRegion)}");
        return ActivatorUtilities.CreateInstance<HistoryPageRegion>(services);
    }
}
