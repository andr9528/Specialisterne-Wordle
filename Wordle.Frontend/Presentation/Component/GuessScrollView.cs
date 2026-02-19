using Wordle.Frontend.Presentation.Component.Logic;
using Wordle.Frontend.Presentation.Component.UserInterface;
using Wordle.Frontend.Presentation.Component.ViewModel;

namespace Wordle.Frontend.Presentation.Component;

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
