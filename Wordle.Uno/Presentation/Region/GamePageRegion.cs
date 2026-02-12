using Wordle.Abstraction.Services;
using Wordle.Uno.Presentation.Region.Logic;
using Wordle.Uno.Presentation.Region.UserInterface;
using Wordle.Uno.Presentation.Region.ViewModel;

namespace Wordle.Uno.Presentation.Region;

public sealed class GamePageRegion : Border
{
    public GamePageRegion()
    {
        DataContext = new GamePageRegionViewModel();

        var gameService = App.Startup.ServiceProvider.GetService<IGameService>() ?? throw new ArgumentNullException(nameof(IGameService));

        Logic = new GamePageRegionLogic(
            this,
            (GamePageRegionViewModel) DataContext, gameService);

        var ui = new GamePageRegionUserInterface(
            Logic,
            (GamePageRegionViewModel) DataContext);

        Child = ui.CreateContent();
    }

    public GamePageRegionLogic Logic { get; }
}
