using Wordle.Abstraction.Interfaces.Model.Entity;
using Wordle.Frontend.Presentation.Core;

namespace Wordle.Frontend.Presentation.Component.ViewModel;

public sealed partial class GuessScrollViewViewModel : BaseViewModel<GuessScrollViewViewModel>
{
    public int MaxGuesses { get; }
    public int WordLength { get; }
    public IGame? CurrentGame { get; }

    public GuessScrollViewViewModel(
        ILogger<GuessScrollViewViewModel> logger, int maxGuesses, int wordLength, IGame? currentGame) : base(logger)
    {
        MaxGuesses = maxGuesses;
        WordLength = wordLength;
        CurrentGame = currentGame;
    }
}
