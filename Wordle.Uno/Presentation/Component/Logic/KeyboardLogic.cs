using Wordle.Uno.Presentation.Component.ViewModel;

namespace Wordle.Uno.Presentation.Component.Logic;

public sealed class KeyboardLogic
{
    private readonly KeyboardViewModel viewModel;

    public KeyboardLogic(KeyboardViewModel viewModel)
    {
        this.viewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));
    }

    // TODO: Implement key press handling, enter/backspace, etc.
}
