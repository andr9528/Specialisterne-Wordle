using System.Collections.ObjectModel;
using Wordle.Uno.Presentation.Core;

namespace Wordle.Uno.Presentation.Component.ViewModel;

public sealed partial class GuessScrollViewViewModel : BaseViewModel
{
    public int MaxGuesses { get; }
    public int WordLength { get; }

    public GuessScrollViewViewModel(int maxGuesses, int wordLength)
    {
        MaxGuesses = maxGuesses;
        WordLength = wordLength;
    }
}
