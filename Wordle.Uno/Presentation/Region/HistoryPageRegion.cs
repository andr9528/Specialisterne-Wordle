using Wordle.Uno.Presentation.Region.Logic;
using Wordle.Uno.Presentation.Region.UserInterface;
using Wordle.Uno.Presentation.Region.ViewModel;

namespace Wordle.Uno.Presentation.Region;

public sealed class HistoryPageRegion : Border
{
    public HistoryPageRegion()
    {
        DataContext = new HistoryPageRegionViewModel();

        Logic = new HistoryPageRegionLogic(
            this,
            (HistoryPageRegionViewModel) DataContext);

        var ui = new HistoryPageRegionUserInterface(
            Logic,
            (HistoryPageRegionViewModel) DataContext);

        Child = ui.CreateContent();
    }

    public HistoryPageRegionLogic Logic { get; }
}
