using Wordle.Abstraction.Interfaces.Persistence;
using Wordle.Abstraction.Services;
using Wordle.Model.Entity;
using Wordle.Model.Searchable;

namespace Wordle.Services;

public class GuessService : IGuessService
{
    public GuessService(IEntityQueryService<Guess, SearchableGuess> guessQueryService, IWordGatherService wordGatherService)
    {
        
    }
}
