using Wordle.Abstraction.Interfaces.Model.Entity;
using Wordle.Uno.Abstraction;
using Wordle.Uno.Presentation.Component.ViewModel;
using Wordle.Uno.Presentation.Core;

namespace Wordle.Uno.Presentation.Region.ViewModel;

public sealed partial class GamePageRegionViewModel : BaseViewModel<GamePageRegionViewModel>
{
    public IGame? CurrentGame { get; set; }
    private readonly IUiDispatcher uiDispatcher;
    private readonly IViewModelFactory vmFactory;
    [ObservableProperty] private GuessScrollViewViewModel guessScrollViewViewModel;
    public event Action? GuessScrollViewViewModelChanged;

    public GamePageRegionViewModel(ILogger<GamePageRegionViewModel> logger, IUiDispatcher uiDispatcher, IViewModelFactory vmFactory) : base(logger)
    {
        this.uiDispatcher = uiDispatcher;
        this.vmFactory = vmFactory;

        guessScrollViewViewModel = vmFactory.CreateGuessScrollViewViewModel(0, 0);
    }

    protected override void OnGameChanged(IGame game)
    {
        var wasCurrentGameNull = CurrentGame == null;
        CurrentGame = game;

        if (game.Guesses.Count != 0 || !wasCurrentGameNull)
        {
            return;
        }

        var maxGuesses = game.AttemptsLeft;
        var wordLength = game.Word.Letters.Count;

        uiDispatcher.Enqueue(() => GuessScrollViewViewModel = vmFactory.CreateGuessScrollViewViewModel(maxGuesses, wordLength, CurrentGame));
    }

    partial void OnGuessScrollViewViewModelChanged(GuessScrollViewViewModel oldValue, GuessScrollViewViewModel newValue)
    {
        GuessScrollViewViewModelChanged?.Invoke();
    }
}
