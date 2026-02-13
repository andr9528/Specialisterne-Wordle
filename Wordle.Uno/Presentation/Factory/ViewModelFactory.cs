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
        return new CharacterIndicatorViewModel(uiDispatcher, character);
    }

    /// <inheritdoc />
    public CharacterIndicatorViewModel CreateCharacterIndicatorViewModel(
        int guessNumber, int letterPosition, char? character = null)
    {
        return new CharacterIndicatorViewModel(uiDispatcher, guessNumber, letterPosition, character);
    }

    /// <inheritdoc />
    public GamePageRegionViewModel CreateGamePageRegionViewModel()
    {
        return new GamePageRegionViewModel(uiDispatcher);
    }

    /// <inheritdoc />
    public InformationBarViewModel CreateInformationBarViewModel()
    {
        return new InformationBarViewModel(uiDispatcher);
    }
}
