using Wordle.Uno.Presentation.Component.Logic;
using Wordle.Uno.Presentation.Component.UserInterface;
using Wordle.Uno.Presentation.Component.ViewModel;

namespace Wordle.Uno.Presentation.Component;

/// <summary>
/// Scrollable container holding multiple guess lines.
/// </summary>
public sealed class GuessScrollView : Border
{
    public GuessScrollView(GuessScrollViewViewModel viewModel)
    {
        DataContext = ViewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));

        Logic = new GuessScrollViewLogic(ViewModel);
        var ui = new GuessScrollViewUserInterface(Logic, ViewModel);

        Child = ui.CreateContent();
    }

    public GuessScrollViewViewModel ViewModel { get; }
    public GuessScrollViewLogic Logic { get; }
}
