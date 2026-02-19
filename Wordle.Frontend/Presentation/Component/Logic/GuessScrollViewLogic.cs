using Wordle.Frontend.Presentation.Component.ViewModel;

namespace Wordle.Frontend.Presentation.Component.Logic;

public sealed class GuessScrollViewLogic
{
    private readonly GuessScrollViewViewModel viewModel;

    public GuessScrollViewLogic(GuessScrollViewViewModel viewModel)
    {
        this.viewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));
    }

    // TODO: Implement later (e.g., add/remove guesses, auto-scroll behavior).
}
