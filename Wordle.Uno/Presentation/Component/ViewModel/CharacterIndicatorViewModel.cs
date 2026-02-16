using Wordle.Abstraction.Enums;
using Wordle.Abstraction.Interfaces.Model.Entity;
using Wordle.Uno.Abstraction;
using Wordle.Uno.Presentation.Core;

namespace Wordle.Uno.Presentation.Component.ViewModel;

public sealed partial class CharacterIndicatorViewModel : BaseViewModel<CharacterIndicatorViewModel>
{
    private readonly IUiDispatcher uiDispatcher;
    private readonly IGame? currentGame;

    public CharacterIndicatorViewModel(ILogger<CharacterIndicatorViewModel> logger, IUiDispatcher uiDispatcher, char? character,
        IGame? currentGame = null) : base(logger)
    {
        logger.LogTrace("Constructing a {CharacterIndicatorViewModelName} for a Keyboard Key '{Character}'.", nameof(CharacterIndicatorViewModel), character);

        this.uiDispatcher = uiDispatcher;
        this.currentGame = currentGame;
        this.character = character.ToString() ?? string.Empty;
        isPartOfKeyboard = true;
        State = CharacterState.UNKNOWN;

        Initialize(currentGame);
    }

    public CharacterIndicatorViewModel(ILogger<CharacterIndicatorViewModel> logger, IUiDispatcher uiDispatcher, int guessNumber, int letterPosition,
        IGame? currentGame = null, char? character = null) : base(logger)
    {
        logger.LogTrace(
            "Constructing a {CharacterIndicatorViewModelName} for a guess letter at guess number '{GuessNumber}' position '{LetterPosition}'.",
            nameof(CharacterIndicatorViewModel), guessNumber, letterPosition + 1);

        this.character = character.ToString() ?? string.Empty;
        this.uiDispatcher = uiDispatcher;
        this.guessNumber = guessNumber;
        this.letterPosition = letterPosition;
        this.currentGame = currentGame;
        isPartOfKeyboard = false;
        State = CharacterState.UNKNOWN;

        Initialize(currentGame);
    }

    [ObservableProperty] private string character;
    private readonly int guessNumber;
    private readonly int letterPosition;
    private readonly bool isPartOfKeyboard;
    [ObservableProperty] private CharacterState state;

    private void Initialize(IGame? game)
    {
        if (game == null)
            return;

        UpdateKeyboardCharacter(game);

        var guess = game.Guesses.FirstOrDefault(x => guessNumber == x.Number);
        if (guess != null)
            UpdateGuessCharacters(guess);

    }

    /// <inheritdoc />
    protected override void OnGuessProcessed(IGame game, IGuess guess)
    {
        base.OnGuessProcessed(game, guess);

        UpdateGuessCharacters(guess);
    }

    private void UpdateGuessCharacters(IGuess guess)
    {
        if (isPartOfKeyboard)
            return;

        if (guessNumber != guess.Number)
            return;

        var letter = guess.Word.Letters.ToList()[letterPosition];
        logger.LogDebug(
            "Updating character and state of guess number '{GuessNumber}' at position '{LetterPosition}' to '{LetterContent}' and '{LetterCharacterState}'.",
            guessNumber, letterPosition + 1, letter.Content, letter.CharacterState);
        uiDispatcher.Enqueue(() => Character = letter.Content.ToString());
        uiDispatcher.Enqueue(() => State = letter.CharacterState);
    }

    /// <inheritdoc />
    protected override void OnGameChanged(IGame game)
    {
        base.OnGameChanged(game);

        UpdateKeyboardCharacter(game);
    }

    private void UpdateKeyboardCharacter(IGame game)
    {
        if (!isPartOfKeyboard)
        {
            return;
        }

        var letter = game.Letters.FirstOrDefault(x => string.Equals(x.Content.ToString(), character, StringComparison.InvariantCultureIgnoreCase));
        if(letter == null)
        {
            logger.LogWarning(
                "Expected to find a '{Character}' from the list of letters in the event - but none was found among the '{LettersCount}' letter.",
                character, game.Letters.Count);
            return;
        }

        logger.LogDebug("Updating state for keyboard key '{Character}' to '{CharacterState}'", character,
            letter.CharacterState);
        uiDispatcher.Enqueue(() => State = letter.CharacterState);
    }
}
