using Windows.UI.Text;
using Wordle.Frontend.Presentation.Component.Logic;
using Wordle.Frontend.Presentation.Component.ViewModel;
using Wordle.Frontend.Presentation.Converter;
using Wordle.Frontend.Presentation.Factory;

namespace Wordle.Frontend.Presentation.Component.UserInterface;

public sealed class CharacterIndicatorUserInterface
{
    private readonly CharacterIndicatorLogic logic;
    private readonly CharacterIndicatorViewModel viewModel;

    public CharacterIndicatorUserInterface(CharacterIndicatorLogic logic, CharacterIndicatorViewModel viewModel)
    {
        this.logic = logic ?? throw new ArgumentNullException(nameof(logic));
        this.viewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));
    }

    public UIElement CreateContent()
    {
        TextBlock text = BuildContentTextBlock();

        var grid = GridFactory.CreateDefaultGrid();
        grid.Children.Add(text);
        SetHostBindings(grid);

        return grid;
    }

    private TextBlock BuildContentTextBlock()
    {
        var text = new TextBlock
        {
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center,
            FontSize = 20,
            FontWeight = new FontWeight(4),
        };

        text.SetBinding(TextBlock.TextProperty, new Binding
        {
            Path = nameof(CharacterIndicatorViewModel.Character),
            Mode = BindingMode.TwoWay,
        });
        return text;
    }

    private void SetHostBindings(Grid grid)
    {
        grid.SetBinding(FrameworkElement.BackgroundProperty, new Binding
        {
            Path = new PropertyPath(nameof(CharacterIndicatorViewModel.State)),
            Mode = BindingMode.TwoWay,
            Converter = new CharacterStateToBrushConverter(),
        });
    }
}
