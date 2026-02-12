using Wordle.Abstraction.Services;
using Wordle.Uno.Presentation.Region.ViewModel;

namespace Wordle.Uno.Presentation.Region.Logic;

public sealed class GamePageRegionLogic : IDisposable
{
    private readonly GamePageRegion region;
    private readonly GamePageRegionViewModel viewModel;
    private readonly IGameService gameService;

    private Action? recreateGuessScrollView;

    public GamePageRegionLogic(GamePageRegion region, GamePageRegionViewModel viewModel, IGameService gameService)
    {
        this.region = region ?? throw new ArgumentNullException(nameof(region));
        this.viewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));
        this.gameService = gameService;

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

    public void Dispose() => UnbindGuessScrollViewRecreation();
}
