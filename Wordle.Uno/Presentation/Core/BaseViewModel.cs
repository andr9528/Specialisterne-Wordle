using CommunityToolkit.Mvvm.Messaging;
using Microsoft.UI.Dispatching;
using Wordle.Abstraction.Interfaces.Model.Entity;
using Wordle.Services;

namespace Wordle.Uno.Presentation.Core;

public abstract partial class BaseViewModel<TViewModel> : ObservableObject, IRecipient<GameChangedMessage>,
    IRecipient<GuessProcessedMessage>, IDisposable where TViewModel : BaseViewModel<TViewModel>
{
    protected readonly ILogger<TViewModel> logger;
    private bool _isRegistered;

    protected BaseViewModel(ILogger<TViewModel> logger)
    {
        this.logger = logger;
        RegisterMessenger();
    }

    /// <summary>
    /// Call this if you want manual control instead of auto-registering in the constructor.
    /// (If you keep auto-register, you likely won’t need this.)
    /// </summary>
    protected void RegisterMessenger()
    {
        if (_isRegistered) return;

        WeakReferenceMessenger.Default.RegisterAll(this);
        _isRegistered = true;
    }

    /// <summary>
    /// Call this when the VM is no longer in use (e.g. navigation away), if you have a deterministic lifecycle.
    /// </summary>
    protected void UnregisterMessenger()
    {
        if (!_isRegistered) return;

        WeakReferenceMessenger.Default.UnregisterAll(this);
        _isRegistered = false;
    }

    void IRecipient<GameChangedMessage>.Receive(GameChangedMessage message) => OnGameChanged(message.Game);

    void IRecipient<GuessProcessedMessage>.Receive(GuessProcessedMessage message) =>
        OnGuessProcessed(message.Game, message.Guess);

    /// <summary>
    /// Override in derived VMs and cherry-pick what you need into primitive properties.
    /// </summary>
    protected virtual void OnGameChanged(IGame changedGame)
    {
        logger.LogTrace("{OnEventName} on thread: {ThreadId}, UI access: {HasAccess}", nameof(OnGameChanged),
            Environment.CurrentManagedThreadId, DispatcherQueue.GetForCurrentThread()?.HasThreadAccess);

        if (changedGame == null) throw new ArgumentNullException(nameof(changedGame));
    }

    /// <summary>
    /// Override in derived VMs and cherry-pick what you need into primitive properties.
    /// </summary>
    protected virtual void OnGuessProcessed(IGame game, IGuess guess)
    {
        logger.LogTrace("{OnEventName} on thread: {ThreadId}, UI access: {HasAccess}", nameof(OnGuessProcessed),
            Environment.CurrentManagedThreadId, DispatcherQueue.GetForCurrentThread()?.HasThreadAccess);

        if (game == null) throw new ArgumentNullException(nameof(game));
        if (guess == null) throw new ArgumentNullException(nameof(guess));
    }

    public void Dispose()
    {
        UnregisterMessenger();
        GC.SuppressFinalize(this);
    }
}
