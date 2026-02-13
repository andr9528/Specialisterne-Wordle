using Wordle.Uno.Presentation.Component.ViewModel;
using Wordle.Uno.Presentation.Region.ViewModel;

namespace Wordle.Uno.Abstraction;

public interface IViewModelFactory
{
    CharacterIndicatorViewModel CreateCharacterIndicatorViewModel(char? character);

    CharacterIndicatorViewModel CreateCharacterIndicatorViewModel(
        int guessNumber, int letterPosition, char? character = null);

    GamePageRegionViewModel CreateGamePageRegionViewModel();

    InformationBarViewModel CreateInformationBarViewModel();
}
