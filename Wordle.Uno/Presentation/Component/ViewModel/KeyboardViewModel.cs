using System.Collections.ObjectModel;
using Wordle.Uno.Presentation.Core;

namespace Wordle.Uno.Presentation.Component.ViewModel;

public sealed partial class KeyboardViewModel : BaseViewModel
{
    public KeyboardViewModel()
    {

    }

    [ObservableProperty] private string currentGuess = string.Empty;
}
