using Wordle.Frontend.Abstraction;
using Wordle.Frontend.Presentation.Region;

namespace Wordle.Frontend.NavigationRegion;

public sealed class GamePageRegionDefinition : IPageRegion
{
    private readonly ILogger<GamePageRegionDefinition> logger;
    private UIElement? region;

    public GamePageRegionDefinition(ILogger<GamePageRegionDefinition> logger)
    {
        this.logger = logger;
    }

    public string DisplayName => "Game";

    public IconElement Icon => new SymbolIcon(Symbol.Play);

    public UIElement CreateControl(IServiceProvider services)
    {
        logger.LogInformation($"Changing page to: {nameof(GamePageRegion)}");
        if (region != null)
        {
            return region;
        }

        region = ActivatorUtilities.CreateInstance<GamePageRegion>(services);
        return region;
    }
}
