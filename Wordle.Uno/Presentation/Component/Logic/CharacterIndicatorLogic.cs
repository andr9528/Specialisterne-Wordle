using Wordle.Uno.Presentation.Component.ViewModel;

namespace Wordle.Uno.Presentation.Component.Logic;

public sealed class CharacterIndicatorLogic
{
    private readonly CharacterIndicatorViewModel viewModel;

    public CharacterIndicatorLogic(CharacterIndicatorViewModel viewModel)
    {
        this.viewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));
    }
}
