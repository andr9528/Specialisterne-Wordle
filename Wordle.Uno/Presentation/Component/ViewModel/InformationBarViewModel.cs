using Wordle.Abstraction.Interfaces.Model.Entity;
using Wordle.Uno.Abstraction;
using Wordle.Uno.Presentation.Core;

namespace Wordle.Uno.Presentation.Component.ViewModel;

public sealed partial class InformationBarViewModel : BaseViewModel<InformationBarViewModel>
{
    private readonly IUiDispatcher uiDispatcher;
    private const string ATTEMPTS_LEFT = "Attempts Left: ";

    public InformationBarViewModel(ILogger<InformationBarViewModel> logger, IUiDispatcher uiDispatcher) : base(logger)
    {
        this.uiDispatcher = uiDispatcher;
        attemptsLeftMessage = ATTEMPTS_LEFT + "Unknown";
    }

    [ObservableProperty] private string? attemptsLeftMessage;

    /// <inheritdoc />
    protected override void OnGameChanged(IGame game)
    {
        base.OnGameChanged(game);

        uiDispatcher.Enqueue(() => attemptsLeftMessage = ATTEMPTS_LEFT + game.AttemptsLeft);
    }
}
