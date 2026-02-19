using Wordle.Abstraction.Interfaces.Model.Entity;
using Wordle.Frontend.Presentation.Core;

namespace Wordle.Frontend.Presentation.Component.ViewModel;

public sealed partial class GuessLineViewModel : BaseViewModel<GuessLineViewModel>
{
    public int GuessNumber { get; }
    public int WordLength { get; }
    public IGame? CurrentGame { get; }

    public GuessLineViewModel(ILogger<GuessLineViewModel> logger, int guessNumber, int wordLength, IGame? currentGame = null) : base(logger)
    {
        GuessNumber = guessNumber;
        WordLength = wordLength;
        CurrentGame = currentGame;
    }

}
