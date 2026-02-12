using Wordle.Abstraction.Enums;
using Wordle.Abstraction.Interfaces.Model.Entity;
using Wordle.Uno.Presentation.Core;

namespace Wordle.Uno.Presentation.Component.ViewModel;

public sealed partial class CharacterIndicatorViewModel : BaseViewModel
{
    /// <summary>
    /// Creates a version for displaying characters on the Game Keyboard.
    /// </summary>
    /// <param name="character">Character to be displayed.</param>
    public CharacterIndicatorViewModel(char? character)
    {
        this.character = character;
        isPartOfKeyboard = true;
    }

    /// <summary>
    /// Creates a version for displaying characters in Guesses.
    /// </summary>
    /// <param name="character">Character to be displayed.</param>
    /// <param name="guessNumber">Which Guess, i.e. row, this indicator is linked to.</param>
    /// <param name="letterPosition">Which Position in the guess, i.e. column, this indicator is linked to.</param>
    public CharacterIndicatorViewModel(int guessNumber, int letterPosition, char? character = null)
    {
        this.character = character;
        this.guessNumber = guessNumber;
        this.letterPosition = letterPosition;
        isPartOfKeyboard = false;
    }

    [ObservableProperty] private char? character;
    private readonly int guessNumber;
    private readonly int letterPosition;
    private readonly bool isPartOfKeyboard;
    [ObservableProperty] private CharacterState state = CharacterState.UNKNOWN;

    /// <inheritdoc />
    protected override void OnGuessProcessed(IGame game, IGuess guess)
    {
        base.OnGuessProcessed(game, guess);

        switch (isPartOfKeyboard)
        {
            case true when character != null:
                state = game.Letters.First(x => x.Content == character).CharacterState;
                break;
            case false when guess.Number == guessNumber:
            {
                var letter = guess.Word.Letters.ToList()[letterPosition];
                character = letter.Content;
                state = letter.CharacterState;
                break;
            }
        }
    }
}
