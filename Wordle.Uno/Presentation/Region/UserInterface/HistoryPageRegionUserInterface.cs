using Windows.UI.Text;
using Wordle.Uno.Presentation.Region.Logic;
using Wordle.Uno.Presentation.Region.ViewModel;

namespace Wordle.Uno.Presentation.Region.UserInterface;

public sealed class HistoryPageRegionUserInterface
{
    private readonly HistoryPageRegionLogic logic;
    private readonly HistoryPageRegionViewModel viewModel;

    public HistoryPageRegionUserInterface(HistoryPageRegionLogic logic, HistoryPageRegionViewModel viewModel)
    {
        this.logic = logic ?? throw new ArgumentNullException(nameof(logic));
        this.viewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));
    }

    public UIElement CreateContent()
    {
        // Placeholder layout until you want history implemented.
        return new Grid
        {
            Padding = new Thickness(16),
            Children =
            {
                new TextBlock
                {
                    Text = "History (TODO)",
                    FontSize = 20,
                    FontWeight = new FontWeight(4),
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Top,
                },
                new TextBlock
                {
                    Text = "TODO: Add history UI here (list of previous games, stats, etc.).",
                    TextWrapping = TextWrapping.Wrap,
                    Margin = new Thickness(0, 40, 0, 0),
                }
            }
        };
    }
}
