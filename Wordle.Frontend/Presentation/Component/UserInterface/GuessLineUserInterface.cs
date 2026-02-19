using Wordle.Frontend.Abstraction;
using Wordle.Frontend.Extensions;
using Wordle.Frontend.Presentation.Component.Logic;
using Wordle.Frontend.Presentation.Component.ViewModel;
using Wordle.Frontend.Presentation.Factory;

namespace Wordle.Frontend.Presentation.Component.UserInterface;

public sealed class GuessLineUserInterface
{
    private readonly GuessLineLogic logic;
    private readonly GuessLineViewModel viewModel;

    public GuessLineUserInterface(GuessLineLogic logic, GuessLineViewModel viewModel)
    {
        this.logic = logic ?? throw new ArgumentNullException(nameof(logic));
        this.viewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));
    }

    public UIElement CreateContent()
    {
        var vmFactory = App.Startup.ServiceProvider.GetRequiredService<IViewModelFactory>();

        var grid = GridFactory.CreateDefaultGrid();

        grid.HorizontalAlignment = HorizontalAlignment.Center;
        grid.VerticalAlignment = VerticalAlignment.Center;

        for (int i = 0; i < viewModel.WordLength; i++)
        {
            grid.ColumnDefinitions.Add(new ColumnDefinition
            {
                Width = GridLength.Auto
            });

            var vm = vmFactory.CreateCharacterIndicatorViewModel(viewModel.GuessNumber, i, currentGame: viewModel.CurrentGame);
            var tile = new CharacterIndicator(vm)
            {
                Width = 44,
                Height = 44,
                BorderThickness = new Thickness(1),
                CornerRadius = new CornerRadius(6),
                Margin = new Thickness(3, 0, 3, 0),
            }.SetColumn(i);

            grid.Children.Add(tile);
        }

        return grid;
    }
}
