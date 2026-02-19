using Wordle.Frontend.Abstraction;
using Wordle.Frontend.Extensions;
using Wordle.Frontend.Presentation.Component.Logic;
using Wordle.Frontend.Presentation.Component.ViewModel;
using Wordle.Frontend.Presentation.Factory;

namespace Wordle.Frontend.Presentation.Component.UserInterface;

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

        Grid inputArea = BuildGuessInput().SetRow(0);
        Grid keyboardGrid = BuildKeyboardGrid().SetRow(1);

        root.Children.Add(inputArea);
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

            var vm = vmFactory.CreateCharacterIndicatorViewModel(c, viewModel.CurrentGame);
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


    private Grid BuildGuessInput()
    {
        var root = GridFactory.CreateDefaultGrid();
        root.Margin = new Thickness(16, 0, 16, 0);

        root.ColumnDefinitions.Add(new ColumnDefinition
        {
            Width = new GridLength(85, GridUnitType.Star)
        });

        root.ColumnDefinitions.Add(new ColumnDefinition
        {
            Width = new GridLength(15, GridUnitType.Star)
        });

        var input = BuildInputTextBlock().SetColumn(0);
        var submit = BuildSubmitButton().SetColumn(1);

        root.Children.Add(input);
        root.Children.Add(submit);

        return root;
    }

    private Button BuildSubmitButton()
    {
        var submit = new Button
        {
            Content = "Guess",
            HorizontalAlignment = HorizontalAlignment.Stretch,
            VerticalAlignment = VerticalAlignment.Center,
            Margin = new Thickness(8, 0, 0, 0),
        };

        submit.Click += (sender, args) => _ = logic.SubmitOnClick(sender, args);

        return submit;
    }



    private UIElement BuildInputTextBlock()
    {
        var hostBorder = new Border
        {
            BorderThickness = new Thickness(8),
            CornerRadius = new CornerRadius(6),
            Padding = new Thickness(4),
            HorizontalAlignment = HorizontalAlignment.Stretch,
        };

        var input = new TextBox
        {
            PlaceholderText = "Type your guess…",
            BorderThickness = new Thickness(0),
            TextAlignment = TextAlignment.Center,
            Background = new SolidColorBrush(Colors.Transparent),
        };

        input.SetBinding(TextBox.TextProperty, new Binding
        {
            Path = nameof(Uno.Presentation.Component.ViewModel.KeyboardViewModel.CurrentGuess),
            Mode = BindingMode.TwoWay,
        });

        hostBorder.SetBinding(Border.BorderBrushProperty, new Binding
        {
            Path = nameof(Uno.Presentation.Component.ViewModel.KeyboardViewModel.InputBorderBrush),
            Mode = BindingMode.TwoWay,
        });

        hostBorder.Child = input;
        return hostBorder;
    }
}
