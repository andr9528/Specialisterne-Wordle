using Wordle.Abstraction.Interfaces.Model.Entity;
using Wordle.Uno.Presentation.Component.ViewModel;
using Wordle.Uno.Presentation.Region.ViewModel;

namespace Wordle.Uno.Abstraction;

public interface IViewModelFactory
{
    /// <summary>
    /// Creates a version for displaying characters on the Game Keyboard.
    /// </summary>
    /// <param name="character">Character to be displayed.</param>
    /// <param name="currentGame"></param>
    /// <returns></returns>
    CharacterIndicatorViewModel CreateCharacterIndicatorViewModel(char? character, IGame? currentGame);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="guessNumber">Which Guess, i.e. row, this indicator is linked to.</param>
    /// <param name="letterPosition">Which Position in the guess, i.e. column, this indicator is linked to.</param>
    /// <param name="currentGame"></param>
    /// <param name="character">Character to be displayed.</param>
    /// <returns></returns>
    CharacterIndicatorViewModel CreateCharacterIndicatorViewModel(
        int guessNumber, int letterPosition, IGame? currentGame = null, char? character = null);

    GamePageRegionViewModel CreateGamePageRegionViewModel();

    InformationBarViewModel CreateInformationBarViewModel(IGame? currentGame);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="maxGuesses"></param>
    /// <param name="wordLength"></param>
    /// <param name="currentGame"></param>
    /// <returns></returns>
    GuessScrollViewViewModel CreateGuessScrollViewViewModel(int maxGuesses, int wordLength, IGame? currentGame = null);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="guessNumber"></param>
    /// <param name="wordLength"></param>
    /// <param name="currentGame"></param>
    /// <returns></returns>
    GuessLineViewModel CreateGuessLineViewModel(int guessNumber, int wordLength, IGame? currentGame);

    KeyboardViewModel CreateKeyboardViewModel(IGame? currentGame);
}
