using Wordle.Abstraction.Interfaces.Model.Entity;
using Wordle.Uno.Abstraction;
using Wordle.Uno.Presentation.Core;

namespace Wordle.Uno.Presentation.Component.ViewModel;

public sealed partial class InformationBarViewModel : BaseViewModel<InformationBarViewModel>
{
    private readonly IUiDispatcher uiDispatcher;
    private IGame? game;
    private const string ATTEMPTS_LEFT = "Attempts Left: ";

    public InformationBarViewModel(ILogger<InformationBarViewModel> logger, IUiDispatcher uiDispatcher, IGame? game = null) : base(logger)
    {
        this.uiDispatcher = uiDispatcher;
        this.game = game;
        AttemptsLeftMessage = ATTEMPTS_LEFT + (game == null ? "Unknown" : game.AttemptsLeft.ToString());
    }

    [ObservableProperty] private string? attemptsLeftMessage;

    /// <inheritdoc />
    protected override void OnGameChanged(IGame changedGame)
    {
        base.OnGameChanged(changedGame);

        this.game = changedGame;

        logger.LogDebug("Updating Attempts left to '{GameAttemptsLeft}'.", ATTEMPTS_LEFT + changedGame.AttemptsLeft);
        uiDispatcher.Enqueue(() => AttemptsLeftMessage = ATTEMPTS_LEFT + changedGame.AttemptsLeft);
    }
}
