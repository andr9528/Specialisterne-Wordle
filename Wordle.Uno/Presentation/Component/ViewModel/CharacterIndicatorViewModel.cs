using Wordle.Abstraction.Enums;
using Wordle.Abstraction.Interfaces.Model.Entity;
using Wordle.Uno.Abstraction;
using Wordle.Uno.Presentation.Core;

namespace Wordle.Uno.Presentation.Component.ViewModel;

public sealed partial class CharacterIndicatorViewModel : BaseViewModel<CharacterIndicatorViewModel>
{
    private readonly IUiDispatcher uiDispatcher;

    public CharacterIndicatorViewModel(ILogger<CharacterIndicatorViewModel> logger, IUiDispatcher uiDispatcher, char? character) : base(logger)
    {
        logger.LogDebug("Constructing a {CharacterIndicatorViewModelName} for a Keyboard Key '{Character}'.", nameof(CharacterIndicatorViewModel), character);

        this.uiDispatcher = uiDispatcher;
        this.character = character.ToString() ?? string.Empty;
        isPartOfKeyboard = true;
    }

    public CharacterIndicatorViewModel(ILogger<CharacterIndicatorViewModel> logger, IUiDispatcher uiDispatcher, int guessNumber, int letterPosition, char? character = null) : base(logger)
    {
        logger.LogDebug(
            "Constructing a {CharacterIndicatorViewModelName} for a guess letter at guess number '{GuessNumber}' position '{LetterPosition}'.",
            nameof(CharacterIndicatorViewModel), guessNumber, letterPosition + 1);

        this.character = character.ToString() ?? string.Empty;
        this.uiDispatcher = uiDispatcher;
        this.guessNumber = guessNumber;
        this.letterPosition = letterPosition;
        isPartOfKeyboard = false;
    }

    [ObservableProperty] private string character;
    private readonly int guessNumber;
    private readonly int letterPosition;
    private readonly bool isPartOfKeyboard;
    [ObservableProperty] private CharacterState state = CharacterState.UNKNOWN;

    /// <inheritdoc />
    protected override void OnGuessProcessed(IGame game, IGuess guess)
    {
        base.OnGuessProcessed(game, guess);

        if (isPartOfKeyboard)
            return;

        var letter = guess.Word.Letters.ToList()[letterPosition];
        logger.LogDebug(
            "Updating character and state of guess number '{GuessNumber}' at position '{LetterPosition}' to '{LetterContent}' and '{LetterCharacterState}'.",
            guessNumber, letterPosition + 1, letter.Content, letter.CharacterState);
        uiDispatcher.Enqueue(() => character = letter.Content.ToString());
        uiDispatcher.Enqueue(() => state = letter.CharacterState);
    }

    /// <inheritdoc />
    protected override void OnGameChanged(IGame game)
    {
        base.OnGameChanged(game);

        if (!isPartOfKeyboard)
        {
            return;
        }

        var characterState = game.Letters.First(x => x.Content.ToString() == character).CharacterState;
        logger.LogDebug("Updating state for keyboard key '{Character}' to '{CharacterState}'", character,
            characterState);
        uiDispatcher.Enqueue(() => state = characterState);
    }
}
