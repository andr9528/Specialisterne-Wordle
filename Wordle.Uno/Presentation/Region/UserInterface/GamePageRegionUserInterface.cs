using Windows.UI.Text;
using Wordle.Uno.Abstraction;
using Wordle.Uno.Extensions;
using Wordle.Uno.Presentation.Component;
using Wordle.Uno.Presentation.Component.ViewModel;
using Wordle.Uno.Presentation.Region.Logic;
using Wordle.Uno.Presentation.Region.ViewModel;

namespace Wordle.Uno.Presentation.Region.UserInterface;

public sealed class GamePageRegionUserInterface
{
    private readonly GamePageRegionLogic logic;
    private readonly GamePageRegionViewModel viewModel;
    private Grid guessHostGrid;

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

        var vm = vmFactory.CreateInformationBarViewModel();
        var informationBar = new InformationBar(vm).SetRow(0);
        guessHostGrid = new Grid()
        {
            VerticalAlignment = VerticalAlignment.Stretch,
            HorizontalAlignment = HorizontalAlignment.Stretch,
        }.SetRow(1);
        var keyboard = new Keyboard(new KeyboardViewModel()).SetRow(2);

        RecreateGuessScrollView();

        logic.BindGuessScrollViewRecreation(RecreateGuessScrollView);
        guessHostGrid.Unloaded += (_, __) => logic.UnbindGuessScrollViewRecreation();

        grid.Children.Add(informationBar);
        grid.Children.Add(guessHostGrid);
        grid.Children.Add(keyboard);

        return grid;
    }

    private void RecreateGuessScrollView()
    {
        guessHostGrid.Children.Clear();
        guessHostGrid.Children.Add(new GuessScrollView(viewModel.GuessScrollViewViewModel));
    }
}
