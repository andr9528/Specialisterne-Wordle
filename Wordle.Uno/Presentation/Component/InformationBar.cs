using Wordle.Uno.Presentation.Component.Logic;
using Wordle.Uno.Presentation.Component.UserInterface;
using Wordle.Uno.Presentation.Component.ViewModel;

namespace Wordle.Uno.Presentation.Component;

/// <summary>
/// Displays information such as status messages, attempt count, etc.
/// </summary>
public sealed class InformationBar : Border
{
    public InformationBar(InformationBarViewModel viewModel)
    {
        ViewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));

        Logic = new InformationBarLogic(ViewModel);
        var ui = new InformationBarUserInterface(Logic, ViewModel);

        Child = ui.CreateContent();
    }

    public InformationBarViewModel ViewModel { get; }
    public InformationBarLogic Logic { get; }
}
