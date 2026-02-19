using Microsoft.UI.Dispatching;
using Wordle.Abstraction.Services;
using Wordle.Frontend.Presentation.Region.ViewModel;

namespace Wordle.Frontend.Presentation.Region.Logic;

public sealed class GamePageRegionLogic : IDisposable
{
    private readonly GamePageRegion region;
    private readonly GamePageRegionViewModel viewModel;
    private readonly IGameService gameService;
    private DispatcherQueue dispatcher;

    private Action? recreateGuessScrollView;
    private Action? recreateInformationBar;

    public GamePageRegionLogic(GamePageRegion region, GamePageRegionViewModel viewModel, IGameService gameService)
    {
        this.region = region ?? throw new ArgumentNullException(nameof(region));
        this.viewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));
        this.gameService = gameService;

        dispatcher = DispatcherQueue.GetForCurrentThread();

        _ = gameService.Initialize();
    }

    public void BindGuessScrollViewRecreation(Action recreate)
    {
        recreateGuessScrollView = recreate ?? throw new ArgumentNullException(nameof(recreate));
        viewModel.GuessScrollViewViewModelChanged += OnGuessScrollViewViewModelChanged;
    }

    public void UnbindGuessScrollViewRecreation()
    {
        viewModel.GuessScrollViewViewModelChanged -= OnGuessScrollViewViewModelChanged;
        recreateGuessScrollView = null;
    }

    private void OnGuessScrollViewViewModelChanged()
    {
        recreateGuessScrollView?.Invoke();
    }

    public void Dispose()
    {
        UnbindInformationBarRecreation();
        UnbindGuessScrollViewRecreation();
    }

    public void UnbindInformationBarRecreation()
    {
        viewModel.InformationBarViewViewModelChanged -= OnInformationBarViewModelChanged;
        recreateInformationBar = null;
    }

    public void BindInformationBarRecreation(Action recreate)
    {
        recreateInformationBar = recreate ?? throw new ArgumentNullException(nameof(recreate));
        viewModel.InformationBarViewViewModelChanged += OnInformationBarViewModelChanged;
    }

    private void OnInformationBarViewModelChanged()
    {
        recreateInformationBar?.Invoke();
    }
}
