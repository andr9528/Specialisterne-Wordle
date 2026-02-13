using Wordle.Uno.Abstraction;
using Wordle.Uno.Extensions;
using Wordle.Uno.Presentation.Component.Logic;
using Wordle.Uno.Presentation.Component.ViewModel;
using Wordle.Uno.Presentation.Factory;

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
        var root = GridFactory.CreateDefaultGrid();

        root.RowSpacing = 8;

        root.RowDefinitions.Add(new RowDefinition
        {
            Height = GridLength.Auto
        });

        root.RowDefinitions.Add(new RowDefinition
        {
            Height = GridLength.Auto
        });

        TextBox input = BuildGuessInput().SetRow(0);
        Grid keyboardGrid = BuildKeyboardGrid().SetRow(1);

        root.Children.Add(input);
        root.Children.Add(keyboardGrid);

        return root;
    }

    private Grid BuildKeyboardGrid()
    {
        var keyboardGrid = GridFactory.CreateDefaultGrid();

        keyboardGrid.HorizontalAlignment = HorizontalAlignment.Center;
        keyboardGrid.VerticalAlignment = VerticalAlignment.Center;

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
        var vmFactory = App.Startup.ServiceProvider.GetRequiredService<IViewModelFactory>();

        var rowGrid = GridFactory.CreateDefaultGrid();

        rowGrid.HorizontalAlignment = HorizontalAlignment.Center;
        rowGrid.Margin = new Thickness(leftIndent, 4, 0, 4);

        for (var i = 0; i < keys.Length; i++)
        {
            rowGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
        }

        for (var i = 0; i < keys.Length; i++)
        {
            var c = keys[i];

            var vm = vmFactory.CreateCharacterIndicatorViewModel(c);
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
