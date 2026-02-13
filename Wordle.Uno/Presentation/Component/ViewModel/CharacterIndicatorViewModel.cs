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
        this.uiDispatcher = uiDispatcher;
        this.character = character.ToString() ?? string.Empty;
        isPartOfKeyboard = true;
    }

    public CharacterIndicatorViewModel(ILogger<CharacterIndicatorViewModel> logger, IUiDispatcher uiDispatcher, int guessNumber, int letterPosition, char? character = null) : base(logger)
    {
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

        switch (isPartOfKeyboard)
        {
            case true when character != null:
                var characterState = game.Letters.First(x => x.Content.ToString() == character).CharacterState;
                uiDispatcher.Enqueue(() => state = characterState);
                break;
            case false when guess.Number == guessNumber:
            {
                var letter = guess.Word.Letters.ToList()[letterPosition];
                uiDispatcher.Enqueue(() => character = letter.Content.ToString());
                uiDispatcher.Enqueue(() => state = letter.CharacterState);
                break;
            }
        }
    }
}
