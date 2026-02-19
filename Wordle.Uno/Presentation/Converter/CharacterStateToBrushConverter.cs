using Wordle.Abstraction.Enums;

namespace Wordle.Uno.Presentation.Converter;

public sealed class CharacterStateToBrushConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        if (value is not CharacterState state)
            state = CharacterState.UNKNOWN;

        return state switch
        {
            CharacterState.UNKNOWN => new SolidColorBrush(Colors.White),
            CharacterState.ABSENT => new SolidColorBrush(Colors.Gray),
            CharacterState.PRESENT => new SolidColorBrush(Colors.Yellow),
            CharacterState.CORRECT => new SolidColorBrush(Colors.Green),
            _ => new SolidColorBrush(Colors.White)
        };
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language) =>
        throw new NotSupportedException();
}
