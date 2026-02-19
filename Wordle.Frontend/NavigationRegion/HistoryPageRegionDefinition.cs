using Wordle.Frontend.Abstraction;
using Wordle.Frontend.Presentation.Region;

namespace Wordle.Frontend.NavigationRegion;

public sealed class HistoryPageRegionDefinition : IPageRegion
{
    private readonly ILogger<HistoryPageRegionDefinition> logger;
    private UIElement? region;

    public HistoryPageRegionDefinition(ILogger<HistoryPageRegionDefinition> logger)
    {
        this.logger = logger;
    }

    public string DisplayName => "History";

    public IconElement Icon => new SymbolIcon(Symbol.World);

    public UIElement CreateControl(IServiceProvider services)
    {
        logger.LogInformation($"Changing page to: {nameof(HistoryPageRegion)}");

        if (region != null)
        {
            return region;
        }

        region = ActivatorUtilities.CreateInstance<HistoryPageRegion>(services);
        return region;
    }
}
