using Wordle.Uno.Extensions;
using Wordle.Uno.Presentation.Component.Logic;
using Wordle.Uno.Presentation.Component.ViewModel;
using Wordle.Uno.Presentation.Factory;

namespace Wordle.Uno.Presentation.Component.UserInterface;

public sealed class GuessScrollViewUserInterface
{
    private readonly GuessScrollViewLogic logic;
    private readonly GuessScrollViewViewModel viewModel;

    public GuessScrollViewUserInterface(GuessScrollViewLogic logic, GuessScrollViewViewModel viewModel)
    {
        this.logic = logic ?? throw new ArgumentNullException(nameof(logic));
        this.viewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));
    }

    public UIElement CreateContent()
    {
        var grid = GridFactory.CreateDefaultGrid();
        grid.RowSpacing = 8;

        for (int i = 0; i < viewModel.MaxGuesses; i++)
        {
            grid.RowDefinitions.Add(new RowDefinition
            {
                Height = GridLength.Auto
            });
            var guessLine = new GuessLine(new GuessLineViewModel(i + 1, viewModel.WordLength)).SetRow(i);
            grid.Children.Add(guessLine);
        }

        return new ScrollViewer
        {
            Content = grid,
            VerticalScrollBarVisibility = ScrollBarVisibility.Visible,
        };
    }
}
