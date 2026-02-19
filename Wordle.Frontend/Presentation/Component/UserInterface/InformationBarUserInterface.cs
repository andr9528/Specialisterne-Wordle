using Wordle.Frontend.Presentation.Component.Logic;
using Wordle.Frontend.Presentation.Component.ViewModel;
using Wordle.Frontend.Presentation.Factory;

namespace Wordle.Frontend.Presentation.Component.UserInterface;

public sealed class InformationBarUserInterface
{
    private readonly InformationBarLogic logic;
    private readonly InformationBarViewModel viewModel;

    public InformationBarUserInterface(InformationBarLogic logic, InformationBarViewModel viewModel)
    {
        this.logic = logic ?? throw new ArgumentNullException(nameof(logic));
        this.viewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));
    }

    public UIElement CreateContent()
    {
        var grid = GridFactory.CreateDefaultGrid();

        grid.RowDefinitions.Add(new RowDefinition() {Height = GridLength.Auto});

        var message = new TextBlock
        {
            VerticalAlignment = VerticalAlignment.Center,
            HorizontalTextAlignment = TextAlignment.Center,
            TextWrapping = TextWrapping.Wrap,
        };
        message.SetBinding(TextBlock.TextProperty, new Binding
        {
            Path = nameof(InformationBarViewModel.AttemptsLeftMessage),
            Mode = BindingMode.TwoWay,
        });

        grid.Children.Add(message);

        return grid;
    }
}
