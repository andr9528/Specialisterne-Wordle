using Wordle.Abstraction.Enums;

namespace Wordle.Uno.Presentation.Component.ViewModel;

public sealed class CharacterIndicatorViewModel
{
    public CharacterIndicatorViewModel()
    {
    }

    public char? Character { get; set; }

    public CharacterState State { get; set; } = CharacterState.UNKNOWN;
}
