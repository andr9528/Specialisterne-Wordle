using System.Collections.ObjectModel;
using Wordle.Uno.Presentation.Core;

namespace Wordle.Uno.Presentation.Component.ViewModel;

public sealed partial class GuessLineViewModel : BaseViewModel<GuessLineViewModel>
{
    public int GuessNumber { get; }
    public int WordLength { get; }

    public GuessLineViewModel(ILogger<GuessLineViewModel> logger, int guessNumber, int wordLength) : base(logger)
    {
        GuessNumber = guessNumber;
        WordLength = wordLength;
    }

}
