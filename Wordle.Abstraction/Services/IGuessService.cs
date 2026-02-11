using Wordle.Abstraction.Interfaces.Model.Entity;

namespace Wordle.Abstraction.Services;

public interface IGuessService
{
    IGuess ProcessGuess(IGame currentGame, string guessedWord);
}
