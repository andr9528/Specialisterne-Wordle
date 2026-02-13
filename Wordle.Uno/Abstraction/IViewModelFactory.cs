using Wordle.Uno.Presentation.Component.ViewModel;
using Wordle.Uno.Presentation.Region.ViewModel;

namespace Wordle.Uno.Abstraction;

public interface IViewModelFactory
{
    /// <summary>
    /// Creates a version for displaying characters on the Game Keyboard.
    /// </summary>
    /// <param name="character">Character to be displayed.</param>
    /// <returns></returns>
    CharacterIndicatorViewModel CreateCharacterIndicatorViewModel(char? character);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="guessNumber">Which Guess, i.e. row, this indicator is linked to.</param>
    /// <param name="letterPosition">Which Position in the guess, i.e. column, this indicator is linked to.</param>
    /// <param name="character">Character to be displayed.</param>
    /// <returns></returns>
    CharacterIndicatorViewModel CreateCharacterIndicatorViewModel(
        int guessNumber, int letterPosition, char? character = null);

    GamePageRegionViewModel CreateGamePageRegionViewModel();

    InformationBarViewModel CreateInformationBarViewModel();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="maxGuesses"></param>
    /// <param name="wordLength"></param>
    /// <returns></returns>
    GuessScrollViewViewModel CreateGuessScrollViewViewModel(int maxGuesses, int wordLength);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="guessNumber"></param>
    /// <param name="wordLength"></param>
    /// <returns></returns>
    GuessLineViewModel CreateGuessLineViewModel(int guessNumber, int wordLength);

    KeyboardViewModel CreateKeyboardViewModel();
}
