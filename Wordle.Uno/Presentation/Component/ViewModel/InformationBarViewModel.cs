using Wordle.Abstraction.Interfaces.Model.Entity;
using Wordle.Uno.Presentation.Core;

namespace Wordle.Uno.Presentation.Component.ViewModel;

public sealed partial class InformationBarViewModel : BaseViewModel
{
    private const string ATTEMPTS_LEFT = "Attempts Left: "; 

    public InformationBarViewModel()
    {
        attemptsLeftMessage = ATTEMPTS_LEFT + "Unknown";
    }

    [ObservableProperty] private string? attemptsLeftMessage;

    /// <inheritdoc />
    protected override void OnGameChanged(IGame game)
    {
        base.OnGameChanged(game);

        attemptsLeftMessage = ATTEMPTS_LEFT + game.AttemptsLeft;
    }
}
