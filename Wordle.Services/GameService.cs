using Wordle.Abstraction.Enums;
using Wordle.Abstraction.Interfaces.Model.Entity;
using Wordle.Abstraction.Interfaces.Persistence;
using Wordle.Abstraction.Services;
using Wordle.Model.Entity;
using Wordle.Model.Searchable;

namespace Wordle.Services;

public class GameService : IGameService
{
    private readonly IEntityQueryService<Game, SearchableGame> gameQueryService;
    private readonly IWordGatherService wordGatherService;
    private readonly IGuessService guessService;
    private static IGame? CurrentGame = null;

    public GameService(IEntityQueryService<Game, SearchableGame> gameQueryService, IWordGatherService wordGatherService, IGuessService guessService)
    {
        this.gameQueryService = gameQueryService;
        this.wordGatherService = wordGatherService;
        this.guessService = guessService;
    }

    public Task StartNewGame()
    {
        return CurrentGame != null ? StartFreshGame() : AbandonCurrentGame();
    }

    private async Task AbandonCurrentGame()
    {
        var word = await GetWordToGuess();
        CurrentGame!.State = State.ABANDONED;
    }

    private async Task StartFreshGame()
    {
        var word = await GetWordToGuess();
    }

    private async Task<IWord> GetWordToGuess()
    {
        IList<IWord> words = await wordGatherService.GetPossibleWords();

        var word = words[Random.Shared.Next(words.Count)];

        return word;
    }
}
