using System.Collections.ObjectModel;
using Wordle.Uno.Presentation.Core;

namespace Wordle.Uno.Presentation.Component.ViewModel;

public sealed partial class GuessScrollViewViewModel : BaseViewModel<GuessScrollViewViewModel>
{
    public int MaxGuesses { get; }
    public int WordLength { get; }

    public GuessScrollViewViewModel(ILogger<GuessScrollViewViewModel> logger, int maxGuesses, int wordLength) : base(logger)
    {
        MaxGuesses = maxGuesses;
        WordLength = wordLength;
    }
}
