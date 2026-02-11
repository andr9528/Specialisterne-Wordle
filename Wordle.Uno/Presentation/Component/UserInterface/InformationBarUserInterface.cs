using Wordle.Uno.Presentation.Component.Logic;
using Wordle.Uno.Presentation.Component.ViewModel;

namespace Wordle.Uno.Presentation.Component.UserInterface;

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
        var grid = new Grid();
        grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
        grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });

        var message = new TextBlock
        {
            VerticalAlignment = VerticalAlignment.Center,
            TextWrapping = TextWrapping.Wrap,
        };
        message.SetBinding(TextBlock.TextProperty, new Binding
        {
            Path = nameof(viewModel.Message),
            Mode = BindingMode.OneWay,
        });

        var attempts = new TextBlock
        {
            VerticalAlignment = VerticalAlignment.Center,
            Margin = new Thickness(12, 0, 0, 0),
        };
        // TODO: Replace with a proper MultiBinding/Converter later if desired.
        attempts.Text = "Attempts";

        grid.Children.Add(message);
        grid.Children.Add(attempts);
        Grid.SetColumn(attempts, 1);

        return grid;
    }
}
