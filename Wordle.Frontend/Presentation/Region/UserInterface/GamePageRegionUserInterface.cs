using Wordle.Frontend.Abstraction;
using Wordle.Frontend.Extensions;
using Wordle.Frontend.Presentation.Component;
using Wordle.Frontend.Presentation.Region.Logic;
using Wordle.Frontend.Presentation.Region.ViewModel;

namespace Wordle.Frontend.Presentation.Region.UserInterface;

public sealed class GamePageRegionUserInterface
{
    private readonly GamePageRegionLogic logic;
    private readonly GamePageRegionViewModel viewModel;
    private Grid guessHostGrid;
    private Grid informationHostGrid;

    public GamePageRegionUserInterface(GamePageRegionLogic logic, GamePageRegionViewModel viewModel)
    {
        this.logic = logic ?? throw new ArgumentNullException(nameof(logic));
        this.viewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));
    }

    public UIElement CreateContent()
    {
        var vmFactory = App.Startup.ServiceProvider.GetRequiredService<IViewModelFactory>();

        var grid = new Grid
        {
            Padding = new Thickness(16),
            RowSpacing = 12,
        };

        grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
        grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
        grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

        informationHostGrid = new Grid()
        {
            VerticalAlignment = VerticalAlignment.Stretch,
            HorizontalAlignment = HorizontalAlignment.Stretch,
        }.SetRow(0);
        guessHostGrid = new Grid()
        {
            VerticalAlignment = VerticalAlignment.Stretch,
            HorizontalAlignment = HorizontalAlignment.Stretch,
        }.SetRow(1);
        var keyboardViewModel = vmFactory.CreateKeyboardViewModel(viewModel.CurrentGame);
        var keyboard = new Keyboard(keyboardViewModel).SetRow(2);

        RecreateInformationBar();
        RecreateGuessScrollView();

        logic.BindInformationBarRecreation(RecreateInformationBar);
        logic.BindGuessScrollViewRecreation(RecreateGuessScrollView);
        informationHostGrid.Unloaded += (_, __) => logic.UnbindInformationBarRecreation();
        guessHostGrid.Unloaded += (_, __) => logic.UnbindGuessScrollViewRecreation();

        grid.Children.Add(informationHostGrid);
        grid.Children.Add(guessHostGrid);
        grid.Children.Add(keyboard);

        return grid;
    }

    private void RecreateGuessScrollView()
    {
        guessHostGrid.Children.Clear();
        guessHostGrid.Children.Add(new GuessScrollView(viewModel.GuessScrollViewViewModel));
    }

    private void RecreateInformationBar()
    {
        informationHostGrid.Children.Clear();
        informationHostGrid.Children.Add(new InformationBar(viewModel.InformationBarViewModel));
    }


}
