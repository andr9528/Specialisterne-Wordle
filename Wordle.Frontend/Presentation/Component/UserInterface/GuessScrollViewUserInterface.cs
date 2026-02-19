using Wordle.Frontend.Abstraction;
using Wordle.Frontend.Extensions;
using Wordle.Frontend.Presentation.Component.Logic;
using Wordle.Frontend.Presentation.Component.ViewModel;
using Wordle.Frontend.Presentation.Factory;

namespace Wordle.Frontend.Presentation.Component.UserInterface;

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
        var vmFactory = App.Startup.ServiceProvider.GetRequiredService<IViewModelFactory>();
        
        var grid = GridFactory.CreateDefaultGrid();
        grid.RowSpacing = 8;

        for (int i = 0; i < viewModel.MaxGuesses; i++)
        {
            grid.RowDefinitions.Add(new RowDefinition
            {
                Height = GridLength.Auto
            });
            var vm = vmFactory.CreateGuessLineViewModel(i + 1, viewModel.WordLength, viewModel.CurrentGame);
            var guessLine = new GuessLine(vm).SetRow(i);
            grid.Children.Add(guessLine);
        }

        return new ScrollViewer
        {
            Content = grid,
            VerticalScrollBarVisibility = ScrollBarVisibility.Visible,
        };
    }
}
