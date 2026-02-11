using Wordle.Uno.Presentation.Component.Logic;
using Wordle.Uno.Presentation.Component.UserInterface;
using Wordle.Uno.Presentation.Component.ViewModel;

namespace Wordle.Uno.Presentation.Component;

/// <summary>
/// On-screen keyboard + input field for the current guess.
/// </summary>
public sealed class Keyboard : Border
{
    public Keyboard(KeyboardViewModel viewModel)
    {
        ViewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));

        Logic = new KeyboardLogic(ViewModel);
        var ui = new KeyboardUserInterface(Logic, ViewModel);

        Child = ui.CreateContent();
    }

    public KeyboardViewModel ViewModel { get; }
    public KeyboardLogic Logic { get; }
}
