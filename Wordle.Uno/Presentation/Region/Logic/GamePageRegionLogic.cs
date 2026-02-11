using Wordle.Uno.Presentation.Region.ViewModel;

namespace Wordle.Uno.Presentation.Region.Logic;

public sealed class GamePageRegionLogic
{
    private readonly GamePageRegion region;
    private readonly GamePageRegionViewModel viewModel;

    public GamePageRegionLogic(GamePageRegion region, GamePageRegionViewModel viewModel)
    {
        this.region = region ?? throw new ArgumentNullException(nameof(region));
        this.viewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));
    }

    // TODO: Implement later (start game, submit guess, update board/keyboard, etc.)
}
