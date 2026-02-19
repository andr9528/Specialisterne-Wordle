using Wordle.Frontend.Presentation.Region.ViewModel;

namespace Wordle.Frontend.Presentation.Region.Logic;

public sealed class HistoryPageRegionLogic
{
    private readonly HistoryPageRegion region;
    private readonly HistoryPageRegionViewModel viewModel;

    public HistoryPageRegionLogic(HistoryPageRegion region, HistoryPageRegionViewModel viewModel)
    {
        this.region = region ?? throw new ArgumentNullException(nameof(region));
        this.viewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));
    }

    // TODO: Implement later (load history, filtering, details, etc.)
}
