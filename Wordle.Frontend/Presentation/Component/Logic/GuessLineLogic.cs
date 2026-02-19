using Wordle.Frontend.Presentation.Component.ViewModel;

namespace Wordle.Frontend.Presentation.Component.Logic;

public sealed class GuessLineLogic
{
    private readonly GuessLineViewModel viewModel;

    public GuessLineLogic(GuessLineViewModel viewModel)
    {
        this.viewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));
    }

    // TODO: Implement later (e.g., apply evaluation results to CharacterIndicatorViewModels).
}
