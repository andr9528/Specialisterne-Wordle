using Wordle.Abstraction.Services;
using Wordle.Frontend.Abstraction;
using Wordle.Frontend.Presentation.Region.Logic;
using Wordle.Frontend.Presentation.Region.UserInterface;
using Wordle.Frontend.Presentation.Region.ViewModel;

namespace Wordle.Frontend.Presentation.Region;

public sealed class GamePageRegion : Border
{
    public GamePageRegion()
    {
        var vmFactory = App.Startup.ServiceProvider.GetRequiredService<IViewModelFactory>();

        DataContext = vmFactory.CreateGamePageRegionViewModel();

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
