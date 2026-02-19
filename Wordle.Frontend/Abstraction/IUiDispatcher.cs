namespace Wordle.Frontend.Abstraction;

public interface IUiDispatcher
{
    void Enqueue(Action action);
}
