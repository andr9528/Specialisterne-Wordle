using Wordle.Uno.Presentation.Region.Logic;
using Wordle.Uno.Presentation.Region.UserInterface;
using Wordle.Uno.Presentation.Region.ViewModel;

namespace Wordle.Uno.Presentation.Region;

public sealed class GamePageRegion : Border
{
    public GamePageRegion()
    {
        DataContext = new GamePageRegionViewModel();

        Logic = new GamePageRegionLogic(
            this,
            (GamePageRegionViewModel) DataContext);

        var ui = new GamePageRegionUserInterface(
            Logic,
            (GamePageRegionViewModel) DataContext);

        Child = ui.CreateContent();
    }

    public GamePageRegionLogic Logic { get; }
}
