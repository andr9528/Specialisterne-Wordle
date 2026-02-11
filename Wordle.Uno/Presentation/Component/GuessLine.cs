using Wordle.Uno.Presentation.Component.Logic;
using Wordle.Uno.Presentation.Component.UserInterface;
using Wordle.Uno.Presentation.Component.ViewModel;

namespace Wordle.Uno.Presentation.Component;

/// <summary>
/// A single guess row, showing one tile per character.
/// </summary>
public sealed class GuessLine : Border
{
    public GuessLine(GuessLineViewModel viewModel)
    {
        ViewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));

        Logic = new GuessLineLogic(ViewModel);
        var ui = new GuessLineUserInterface(Logic, ViewModel);

        Child = ui.CreateContent();
    }

    public GuessLineViewModel ViewModel { get; }
    public GuessLineLogic Logic { get; }
}
