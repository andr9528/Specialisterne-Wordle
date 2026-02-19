using Wordle.Frontend.Presentation.Component.Logic;
using Wordle.Frontend.Presentation.Component.UserInterface;
using Wordle.Frontend.Presentation.Component.ViewModel;

namespace Wordle.Frontend.Presentation.Component;

/// <summary>
/// A single guess row, showing one tile per character.
/// </summary>
public sealed class GuessLine : Border
{
    public GuessLine(GuessLineViewModel viewModel)
    {
        DataContext = ViewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));

        Logic = new GuessLineLogic(ViewModel);
        var ui = new GuessLineUserInterface(Logic, ViewModel);

        Child = ui.CreateContent();
    }

    public GuessLineViewModel ViewModel { get; }
    public GuessLineLogic Logic { get; }
}
