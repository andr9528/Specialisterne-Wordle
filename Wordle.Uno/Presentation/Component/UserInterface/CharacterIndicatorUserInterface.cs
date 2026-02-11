using Windows.UI.Text;
using Wordle.Uno.Presentation.Component.Logic;
using Wordle.Uno.Presentation.Component.ViewModel;

namespace Wordle.Uno.Presentation.Component.UserInterface;

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
            Mode = BindingMode.OneWay,
            Converter = new NullableCharToStringConverter(),
        });

        return new Grid
        {
            Children = { text }
        };
    }

    private sealed class NullableCharToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
            => value is char c ? c.ToString() : string.Empty;

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            var s = value as string;
            if (string.IsNullOrWhiteSpace(s))
            {
                return null!;
            }

            return s.Trim()[0];
        }
    }
}
