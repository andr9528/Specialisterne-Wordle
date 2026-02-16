using Wordle.Uno.Presentation.Component.Logic;
using Wordle.Uno.Presentation.Component.UserInterface;
using Wordle.Uno.Presentation.Component.ViewModel;

namespace Wordle.Uno.Presentation.Component;

/// <summary>
/// A single letter tile that can represent a guessed character and its state (absent/present/correct).
/// </summary>
public sealed class CharacterIndicator : Border
{
    public CharacterIndicator(CharacterIndicatorViewModel viewModel)
    {
        DataContext = ViewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));

        Logic = new CharacterIndicatorLogic(ViewModel);
        var ui = new CharacterIndicatorUserInterface(Logic, ViewModel);

        Child = ui.CreateContent();
    }

    public CharacterIndicatorViewModel ViewModel { get; }
    public CharacterIndicatorLogic Logic { get; }
}
