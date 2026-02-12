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

        var message = new TextBlock
        {
            VerticalAlignment = VerticalAlignment.Center,
            TextWrapping = TextWrapping.Wrap,
        };
        message.SetBinding(TextBlock.TextProperty, new Binding
        {
            Path = nameof(viewModel.AttemptsLeftMessage),
            Mode = BindingMode.OneWay,
        });

        grid.Children.Add(message);

        return grid;
    }
}
