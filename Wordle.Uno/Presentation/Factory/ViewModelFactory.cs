using Wordle.Uno.Abstraction;
using Wordle.Uno.Presentation.Component.ViewModel;
using Wordle.Uno.Presentation.Region.ViewModel;

namespace Wordle.Uno.Presentation.Factory;

public class ViewModelFactory : IViewModelFactory
{
    private readonly IUiDispatcher uiDispatcher;

    public ViewModelFactory(IUiDispatcher uiDispatcher)
    {
        this.uiDispatcher = uiDispatcher;
    }

    /// <inheritdoc />
    public CharacterIndicatorViewModel CreateCharacterIndicatorViewModel(char? character)
    {
        var logger = App.Startup.ServiceProvider.GetRequiredService<ILogger<CharacterIndicatorViewModel>>();
        return new CharacterIndicatorViewModel(logger, uiDispatcher, character);
    }

    /// <inheritdoc />
    public CharacterIndicatorViewModel CreateCharacterIndicatorViewModel(
        int guessNumber, int letterPosition, char? character = null)
    {
        var logger = App.Startup.ServiceProvider.GetRequiredService<ILogger<CharacterIndicatorViewModel>>();
        return new CharacterIndicatorViewModel(logger, uiDispatcher, guessNumber, letterPosition, character);
    }

    /// <inheritdoc />
    public GamePageRegionViewModel CreateGamePageRegionViewModel()
    {
        var logger = App.Startup.ServiceProvider.GetRequiredService<ILogger<GamePageRegionViewModel>>();
        return new GamePageRegionViewModel(logger, uiDispatcher, this);
    }

    /// <inheritdoc />
    public InformationBarViewModel CreateInformationBarViewModel()
    {
        var logger = App.Startup.ServiceProvider.GetRequiredService<ILogger<InformationBarViewModel>>();
        return new InformationBarViewModel(logger, uiDispatcher);
    }

    /// <inheritdoc />
    public GuessScrollViewViewModel CreateGuessScrollViewViewModel(int maxGuesses, int wordLength)
    {
        var logger = App.Startup.ServiceProvider.GetRequiredService<ILogger<GuessScrollViewViewModel>>();
        return new GuessScrollViewViewModel(logger, maxGuesses, wordLength);
    }

    /// <param name="guessNumber"></param>
    /// <param name="wordLength"></param>
    /// <inheritdoc />
    public GuessLineViewModel CreateGuessLineViewModel(int guessNumber, int wordLength)
    {
        var logger = App.Startup.ServiceProvider.GetRequiredService<ILogger<GuessLineViewModel>>();
        return new GuessLineViewModel(logger, guessNumber, wordLength);
    }

    /// <inheritdoc />
    public KeyboardViewModel CreateKeyboardViewModel()
    {
        var logger = App.Startup.ServiceProvider.GetRequiredService<ILogger<KeyboardViewModel>>();
        return new KeyboardViewModel(logger);
    }
}
