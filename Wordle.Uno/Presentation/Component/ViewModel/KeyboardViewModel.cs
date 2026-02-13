using System.Collections.ObjectModel;
using Wordle.Uno.Presentation.Core;

namespace Wordle.Uno.Presentation.Component.ViewModel;

public sealed partial class KeyboardViewModel : BaseViewModel<KeyboardViewModel>
{
    public KeyboardViewModel(ILogger<KeyboardViewModel> logger) : base(logger)
    {

    }

    [ObservableProperty] private string currentGuess = string.Empty;
    [ObservableProperty] private Brush inputBorderBrush = new SolidColorBrush(Colors.Gray);
}
