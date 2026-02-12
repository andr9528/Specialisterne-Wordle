using System.Collections.ObjectModel;
using Wordle.Uno.Presentation.Core;

namespace Wordle.Uno.Presentation.Component.ViewModel;

public sealed partial class GuessLineViewModel : BaseViewModel
{
    public int GuessNumber { get; }
    public int WordLength { get; }

    public GuessLineViewModel(int guessNumber, int wordLength)
    {
        GuessNumber = guessNumber;
        WordLength = wordLength;
    }

}
