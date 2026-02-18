using Wordle.Uno.Abstraction;
using Wordle.Uno.Presentation.Region;

namespace Wordle.Uno.NavigationRegion;

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
