namespace Wordle.Uno.Presentation.Component.ViewModel;

public sealed class InformationBarViewModel
{
    public InformationBarViewModel()
    {
    }

    public string Message { get; set; } = string.Empty;

    public int AttemptsUsed { get; set; }

    public int MaxAttempts { get; set; } = 6;
}
