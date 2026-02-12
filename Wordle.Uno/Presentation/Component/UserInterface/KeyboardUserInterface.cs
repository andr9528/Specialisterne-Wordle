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

        TextBox input = BuildGuessInput();

        root.Children.Add(input);

        Grid keyboardGrid = BuildKeyboardGrid();

        root.Children.Add(keyboardGrid);

        return root;
    }

    private Grid BuildKeyboardGrid()
    {
        var keyboardGrid = new Grid
        {
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center,
        };

        keyboardGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
        keyboardGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
        keyboardGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

        var row1 = BuildKeyboardRow("QWERTYUIOP");
        Grid.SetRow(row1, 0);
        keyboardGrid.Children.Add(row1);

        var row2 = BuildKeyboardRow("ASDFGHJKL", leftIndent: 16);
        Grid.SetRow(row2, 1);
        keyboardGrid.Children.Add(row2);

        var row3 = BuildKeyboardRow("ZXCVBNM", leftIndent: 32);
        Grid.SetRow(row3, 2);
        keyboardGrid.Children.Add(row3);

        return keyboardGrid;
    }

    private Grid BuildKeyboardRow(string keys, double leftIndent = 0)
    {
        var rowGrid = new Grid
        {
            HorizontalAlignment = HorizontalAlignment.Center,
            Margin = new Thickness(leftIndent, 4, 0, 4),
        };

        // One column per key
        for (var i = 0; i < keys.Length; i++)
        {
            rowGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
        }

        for (var i = 0; i < keys.Length; i++)
        {
            var c = keys[i];

            var vm = new CharacterIndicatorViewModel(c);
            var key = new CharacterIndicator(vm)
            {
                Width = 36,
                Height = 44,
                CornerRadius = new CornerRadius(6),
                BorderThickness = new Thickness(1),
                BorderBrush = new SolidColorBrush(Colors.DimGray),
                Margin = new Thickness(2),
            };

            Grid.SetColumn(key, i);
            rowGrid.Children.Add(key);
        }

        return rowGrid;
    }


    private TextBox BuildGuessInput()
    {
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
        return input;
    }
}
