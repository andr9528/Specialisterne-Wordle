using Wordle.Uno.Presentation.Component.Logic;
using Wordle.Uno.Presentation.Component.ViewModel;

namespace Wordle.Uno.Presentation.Component.UserInterface;

public sealed class KeyboardUserInterface
{
    private readonly KeyboardLogic logic;
    private readonly KeyboardViewModel viewModel;

    public KeyboardUserInterface(KeyboardLogic logic, KeyboardViewModel viewModel)
    {
        this.logic = logic ?? throw new ArgumentNullException(nameof(logic));
        this.viewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));
    }

    public UIElement CreateContent()
    {
        var root = new StackPanel
        {
            Spacing = 8,
        };

        var input = new TextBox
        {
            PlaceholderText = "Type your guess…",
        };

        input.SetBinding(TextBox.TextProperty, new Binding
        {
            Path = nameof(viewModel.CurrentGuess),
            Mode = BindingMode.TwoWay,
            UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
        });

        root.Children.Add(input);

        // Simple UniformGrid-like layout using Grid. You can replace with ItemsRepeater later.
        var keysGrid = new Grid();
        for (int col = 0; col < 10; col++) keysGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
        for (int row = 0; row < 3; row++) keysGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

        // NOTE: Actual button click wiring is intentionally left for later.
        int i = 0;
        foreach (var key in viewModel.Keys)
        {
            var btn = new Button { Content = key.ToString(), Margin = new Thickness(2) };
            Grid.SetRow(btn, i / 10);
            Grid.SetColumn(btn, i % 10);
            keysGrid.Children.Add(btn);
            i++;
            if (i >= 30) break;
        }

        root.Children.Add(keysGrid);

        return root;
    }
}
