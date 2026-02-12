using Windows.UI.Text;
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
    private Grid guessHost;

    public GamePageRegionUserInterface(GamePageRegionLogic logic, GamePageRegionViewModel viewModel)
    {
        this.logic = logic ?? throw new ArgumentNullException(nameof(logic));
        this.viewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));
    }

    public UIElement CreateContent()
    {
        var grid = new Grid
        {
            Padding = new Thickness(16),
            RowSpacing = 12,
        };

        grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
        grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
        grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

        var informationBar = new InformationBar(new InformationBarViewModel()).SetRow(0);
        guessHost = new Grid().SetRow(1);
        var keyboard = new Keyboard(new KeyboardViewModel());

        RecreateGuessScrollView();

        logic.BindGuessScrollViewRecreation(RecreateGuessScrollView);
        guessHost.Unloaded += (_, __) => logic.UnbindGuessScrollViewRecreation();

        grid.Children.Add(informationBar);
        grid.Children.Add(guessHost);
        grid.Children.Add(keyboard);

        return grid;
    }

    private void RecreateGuessScrollView()
    {
        guessHost.Children.Clear();
        guessHost.Children.Add(new GuessScrollView(viewModel.GuessScrollViewViewModel));
    }
}
