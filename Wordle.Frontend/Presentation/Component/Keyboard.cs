using Wordle.Frontend.Presentation.Component.Logic;
using Wordle.Frontend.Presentation.Component.UserInterface;
using Wordle.Frontend.Presentation.Component.ViewModel;

namespace Wordle.Frontend.Presentation.Component;

/// <summary>
/// On-screen keyboard + input field for the current guess.
/// </summary>
public sealed class Keyboard : Border
{
    public Keyboard(KeyboardViewModel viewModel)
    {
        DataContext = ViewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));

        Logic = ActivatorUtilities.CreateInstance<KeyboardLogic>(App.Startup.ServiceProvider, viewModel);
        var ui = new KeyboardUserInterface(Logic, ViewModel);

        Child = ui.CreateContent();
    }

    public KeyboardViewModel ViewModel { get; }
    public KeyboardLogic Logic { get; }
}
