using Microsoft.UI.Dispatching;
using Wordle.Frontend.Abstraction;

namespace Wordle.Frontend.Presentation.Core;

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
