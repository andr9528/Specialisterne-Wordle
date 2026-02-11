using Windows.UI.Text;
using Wordle.Uno.Presentation.Region.Logic;
using Wordle.Uno.Presentation.Region.ViewModel;

namespace Wordle.Uno.Presentation.Region.UserInterface;

public sealed class GamePageRegionUserInterface
{
    private readonly GamePageRegionLogic logic;
    private readonly GamePageRegionViewModel viewModel;

    public GamePageRegionUserInterface(GamePageRegionLogic logic, GamePageRegionViewModel viewModel)
    {
        this.logic = logic ?? throw new ArgumentNullException(nameof(logic));
        this.viewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));
    }

    public UIElement CreateContent()
    {
        // Placeholder layout. Replace with your real composition:
        // - InformationBar
        // - GuessScrollView (guess lines)
        // - Keyboard
        var grid = new Grid
        {
            Padding = new Thickness(16),
            RowSpacing = 12,
        };

        grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
        grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
        grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

        var header = new TextBlock
        {
            Text = "Game (TODO)",
            FontSize = 20,
            FontWeight = new FontWeight(4),
        };

        var body = new TextBlock
        {
            Text = "TODO: Add game UI here (guess grid + keyboard).",
            TextWrapping = TextWrapping.Wrap,
            VerticalAlignment = VerticalAlignment.Center,
        };

        var footer = new TextBlock
        {
            Text = "TODO: Status / info bar area.",
            Opacity = 0.8,
        };

        grid.Children.Add(header);
        Grid.SetRow(header, 0);

        grid.Children.Add(body);
        Grid.SetRow(body, 1);

        grid.Children.Add(footer);
        Grid.SetRow(footer, 2);

        return grid;
    }
}
