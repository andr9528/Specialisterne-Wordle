using System.Collections.ObjectModel;

namespace Wordle.Uno.Presentation.Component.ViewModel;

public sealed class GuessScrollViewViewModel
{
    public GuessScrollViewViewModel()
    {
        Guesses = new ObservableCollection<GuessLineViewModel>();
    }

    public ObservableCollection<GuessLineViewModel> Guesses { get; }
}
