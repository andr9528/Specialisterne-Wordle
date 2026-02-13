using Windows.UI.Text;
using Wordle.Uno.Presentation.Component.Logic;
using Wordle.Uno.Presentation.Component.ViewModel;
using Wordle.Uno.Presentation.Converter;
using Wordle.Uno.Presentation.Factory;

namespace Wordle.Uno.Presentation.Component.UserInterface;

public sealed class CharacterIndicatorUserInterface
{
    private readonly CharacterIndicatorLogic logic;
    private readonly CharacterIndicatorViewModel viewModel;
    private readonly Border host;

    public CharacterIndicatorUserInterface(CharacterIndicatorLogic logic, CharacterIndicatorViewModel viewModel,
        Border host)
    {
        this.logic = logic ?? throw new ArgumentNullException(nameof(logic));
        this.viewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));
        this.host = host ?? throw new ArgumentNullException(nameof(host));
    }

    public UIElement CreateContent()
    {
        SetHostBindings();
        TextBlock text = BuildContentTextBlock();

        var grid = GridFactory.CreateDefaultGrid();
        grid.Children.Add(text);

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
            Path = nameof(viewModel.Character),
            Mode = BindingMode.TwoWay,
        });
        return text;
    }

    private void SetHostBindings()
    {
        host.SetBinding(FrameworkElement.BackgroundProperty, new Binding
        {
            Path = new PropertyPath(nameof(CharacterIndicatorViewModel.State)),
            Mode = BindingMode.TwoWay,
            Converter = new CharacterStateToBrushConverter(),
        });
    }
}
