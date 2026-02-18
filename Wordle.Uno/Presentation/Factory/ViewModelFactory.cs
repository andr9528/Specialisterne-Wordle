using Wordle.Abstraction.Interfaces.Model.Entity;
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
    public CharacterIndicatorViewModel CreateCharacterIndicatorViewModel(char? character, IGame? currentGame = null)
    {
        var logger = App.Startup.ServiceProvider.GetRequiredService<ILogger<CharacterIndicatorViewModel>>();
        return new CharacterIndicatorViewModel(logger, uiDispatcher, character, currentGame);
    }

    /// <inheritdoc />
    public CharacterIndicatorViewModel CreateCharacterIndicatorViewModel(
        int guessNumber, int letterPosition, IGame? currentGame = null, char? character = null)
    {
        var logger = App.Startup.ServiceProvider.GetRequiredService<ILogger<CharacterIndicatorViewModel>>();
        return new CharacterIndicatorViewModel(logger, uiDispatcher, guessNumber, letterPosition, currentGame, character);
    }

    /// <inheritdoc />
    public GamePageRegionViewModel CreateGamePageRegionViewModel()
    {
        var logger = App.Startup.ServiceProvider.GetRequiredService<ILogger<GamePageRegionViewModel>>();
        return new GamePageRegionViewModel(logger, uiDispatcher, this);
    }

    /// <inheritdoc />
    public InformationBarViewModel CreateInformationBarViewModel(IGame? currentGame = null)
    {
        var logger = App.Startup.ServiceProvider.GetRequiredService<ILogger<InformationBarViewModel>>();
        return new InformationBarViewModel(logger, uiDispatcher, currentGame);
    }

    /// <inheritdoc />
    public GuessScrollViewViewModel CreateGuessScrollViewViewModel(int maxGuesses, int wordLength, IGame? currentGame = null)
    {
        var logger = App.Startup.ServiceProvider.GetRequiredService<ILogger<GuessScrollViewViewModel>>();
        return new GuessScrollViewViewModel(logger, maxGuesses, wordLength, currentGame);
    }

    /// <param name="guessNumber"></param>
    /// <param name="wordLength"></param>
    /// <param name="currentGame"></param>
    /// <inheritdoc />
    public GuessLineViewModel CreateGuessLineViewModel(int guessNumber, int wordLength, IGame? currentGame = null)
    {
        var logger = App.Startup.ServiceProvider.GetRequiredService<ILogger<GuessLineViewModel>>();
        return new GuessLineViewModel(logger, guessNumber, wordLength, currentGame);
    }

    /// <param name="currentGame"></param>
    /// <inheritdoc />
    public KeyboardViewModel CreateKeyboardViewModel(IGame currentGame)
    {
        var logger = App.Startup.ServiceProvider.GetRequiredService<ILogger<KeyboardViewModel>>();
        return new KeyboardViewModel(logger, currentGame);
    }
}
