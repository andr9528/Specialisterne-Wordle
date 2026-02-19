using Wordle.Frontend.Presentation.Region.Logic;
using Wordle.Frontend.Presentation.Region.UserInterface;
using Wordle.Frontend.Presentation.Region.ViewModel;

namespace Wordle.Frontend.Presentation.Region;

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
