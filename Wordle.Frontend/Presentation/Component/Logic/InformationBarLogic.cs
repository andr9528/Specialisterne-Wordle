using Wordle.Frontend.Presentation.Component.ViewModel;

namespace Wordle.Frontend.Presentation.Component.Logic;

public sealed class InformationBarLogic
{
    private readonly InformationBarViewModel viewModel;

    public InformationBarLogic(InformationBarViewModel viewModel)
    {
        this.viewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));
    }

    // TODO: Implement later (e.g., message lifecycle, timers, etc.).
}
