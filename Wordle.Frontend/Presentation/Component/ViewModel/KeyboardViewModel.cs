using Wordle.Abstraction.Interfaces.Model.Entity;
using Wordle.Frontend.Presentation.Core;

namespace Wordle.Frontend.Presentation.Component.ViewModel;

public sealed partial class KeyboardViewModel : BaseViewModel<KeyboardViewModel>
{
    public IGame? CurrentGame { get; }

    public KeyboardViewModel(ILogger<KeyboardViewModel> logger, IGame? currentGame = null) : base(logger)
    {
        CurrentGame = currentGame;
    }

    [ObservableProperty] private string currentGuess = string.Empty;
    [ObservableProperty] private Brush inputBorderBrush = new SolidColorBrush(Colors.Gray);

    /// <inheritdoc />
    protected override void OnGuessProcessed(IGame game, IGuess guess)
    {
        base.OnGuessProcessed(game, guess);

        currentGuess = string.Empty;
    }
}
