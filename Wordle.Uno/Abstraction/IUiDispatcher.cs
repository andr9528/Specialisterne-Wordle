namespace Wordle.Uno.Abstraction;

public interface IUiDispatcher
{
    void Enqueue(Action action);
}
