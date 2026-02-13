using Microsoft.UI.Dispatching;
using Wordle.Uno.Abstraction;

namespace Wordle.Uno.Presentation.Core;

public sealed class UiDispatcher : IUiDispatcher
{
    private readonly DispatcherQueue dispatcher;

    public UiDispatcher(DispatcherQueue dispatcher) => this.dispatcher = dispatcher;

    public void Enqueue(Action action)
    {
        if (dispatcher.HasThreadAccess) action();
        else dispatcher.TryEnqueue(() => action());
    }
}
