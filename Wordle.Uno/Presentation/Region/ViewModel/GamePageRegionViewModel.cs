using Wordle.Abstraction.Interfaces.Model.Entity;
using Wordle.Uno.Presentation.Component.ViewModel;
using Wordle.Uno.Presentation.Core;

namespace Wordle.Uno.Presentation.Region.ViewModel;

public sealed partial class GamePageRegionViewModel : BaseViewModel
{
    [ObservableProperty] private GuessScrollViewViewModel guessScrollViewViewModel = new(0, 0);
    public event Action? GuessScrollViewViewModelChanged;

    protected override void OnGameChanged(IGame game)
    {
        if (game.Guesses.Count != 0)
        {
            return;
        }

        var maxGuesses = game.AttemptsLeft;
        var wordLength = game.Word.Letters.Count;

        GuessScrollViewViewModel = new GuessScrollViewViewModel(maxGuesses, wordLength);
    }

    partial void OnGuessScrollViewViewModelChanged(GuessScrollViewViewModel oldValue, GuessScrollViewViewModel newValue)
    {
        GuessScrollViewViewModelChanged?.Invoke();
    }
}
