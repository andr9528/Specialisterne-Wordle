using Wordle.Abstraction.Interfaces.Model.Entity;

namespace Wordle.Abstraction.Services;

public interface IWordGatherService
{
    Task<IList<IWord>> GetPossibleWords();
}
