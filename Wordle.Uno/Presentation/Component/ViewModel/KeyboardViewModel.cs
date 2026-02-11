using System.Collections.ObjectModel;

namespace Wordle.Uno.Presentation.Component.ViewModel;

public sealed class KeyboardViewModel
{
    public KeyboardViewModel()
    {
        Keys = new ObservableCollection<char>();
    }

    public string CurrentGuess { get; set; } = string.Empty;

    public ObservableCollection<char> Keys { get; }

    // Convenience: typical QWERTY layout (you can replace with your own locale/layout later).
    public void LoadDefaultKeys()
    {
        Keys.Clear();
        foreach (var c in "QWERTYUIOPASDFGHJKLZXCVBNM")
        {
            Keys.Add(c);
        }
    }
}
