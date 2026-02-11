using Wordle.Abstraction.Interfaces.Model.Entity;

namespace Wordle.Abstraction.Services;

public interface IWordService
{
    Task<IWord> GetRandomWord();
    Task<bool> IsGuessedWordValid(string guess);
    Task<IList<string>> GetWords();
}
