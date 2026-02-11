using System.Collections.ObjectModel;

namespace Wordle.Uno.Presentation.Component.ViewModel;

public sealed class GuessLineViewModel
{
    public GuessLineViewModel(int wordLength)
    {
        if (wordLength <= 0) throw new ArgumentOutOfRangeException(nameof(wordLength));

        Characters = new ObservableCollection<CharacterIndicatorViewModel>();
        for (int i = 0; i < wordLength; i++)
        {
            Characters.Add(new CharacterIndicatorViewModel());
        }
    }

    public ObservableCollection<CharacterIndicatorViewModel> Characters { get; }
}
