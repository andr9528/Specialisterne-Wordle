using Wordle.Abstraction.Interfaces.Model.Entity;
using Wordle.Frontend.Abstraction;
using Wordle.Frontend.Presentation.Component.ViewModel;
using Wordle.Frontend.Presentation.Core;

namespace Wordle.Frontend.Presentation.Region.ViewModel;

public sealed partial class GamePageRegionViewModel : BaseViewModel<GamePageRegionViewModel>
{
    public IGame? CurrentGame { get; set; }
    private readonly IUiDispatcher uiDispatcher;
    private readonly IViewModelFactory vmFactory;
    [ObservableProperty] private GuessScrollViewViewModel guessScrollViewViewModel;
    [ObservableProperty] private InformationBarViewModel informationBarViewModel;
    public event Action? GuessScrollViewViewModelChanged;
    public event Action? InformationBarViewViewModelChanged;

    public GamePageRegionViewModel(ILogger<GamePageRegionViewModel> logger, IUiDispatcher uiDispatcher, IViewModelFactory vmFactory) : base(logger)
    {
        this.uiDispatcher = uiDispatcher;
        this.vmFactory = vmFactory;

        guessScrollViewViewModel = vmFactory.CreateGuessScrollViewViewModel(0, 0);
        informationBarViewModel = vmFactory.CreateInformationBarViewModel(null);
    }

    protected override void OnGameChanged(IGame changedGame)
    {
        var isFirstEvent = CurrentGame is null;

        CurrentGame = changedGame;

        if (isFirstEvent)
        {
            UpdateInformationBarViewModel();
            UpdateGuessScrollViewViewModel(changedGame);
            return;
        }

        if (changedGame.Guesses.Count == 0)
        {
            UpdateGuessScrollViewViewModel(changedGame);
        }
    }

    private void UpdateInformationBarViewModel()
    {
        uiDispatcher.Enqueue(() => InformationBarViewModel = vmFactory.CreateInformationBarViewModel(CurrentGame));
    }

    private void UpdateGuessScrollViewViewModel(IGame changedGame)
    {
        var maxGuesses = changedGame.AttemptsLeft + changedGame.Guesses.Count;
        var wordLength = changedGame.Word.Letters.Count;

        uiDispatcher.Enqueue(() =>
            GuessScrollViewViewModel =
                vmFactory.CreateGuessScrollViewViewModel(maxGuesses, wordLength, CurrentGame));
    }

    partial void OnGuessScrollViewViewModelChanged(GuessScrollViewViewModel oldValue, GuessScrollViewViewModel newValue)
    {
        GuessScrollViewViewModelChanged?.Invoke();
    }

    partial void OnInformationBarViewModelChanged(InformationBarViewModel oldvalue, InformationBarViewModel newValue)
    {
        InformationBarViewViewModelChanged?.Invoke();
    }
}
