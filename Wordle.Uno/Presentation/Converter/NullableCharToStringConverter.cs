namespace Wordle.Uno.Presentation.Converter;

public sealed class NullableCharToStringConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language) =>
        value is char c ? c.ToString() : string.Empty;

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
