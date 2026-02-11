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
    private readonly IWordService wordService;
    private readonly IGuessService guessService;
    private static Game? _currentGame = null;

    public GameService(IEntityQueryService<Game, SearchableGame> gameQueryService, IWordService wordService, IGuessService guessService)
    {
        this.gameQueryService = gameQueryService;
        this.wordService = wordService;
        this.guessService = guessService;
    }

    public Task StartNewGame()
    {
        return _currentGame != null ? StartFreshGame() : AbandonCurrentGame();
    }

    /// <inheritdoc />
    public async Task<bool> ProcessGuess(string guessedWord)
    {
        if (!await wordService.IsGuessedWordValid(guessedWord)) return false;

        var guess = guessService.ProcessGuess(_currentGame, guessedWord);
        _currentGame.Guesses.Add(guess);
        _currentGame.AttemptsLeft--;

        if (guess.Word.Letters.All(x => x.CorrectPosition))
            _currentGame.State = State.WON;
        else if (_currentGame.AttemptsLeft <= 0)
            _currentGame.State = State.LOST;

        await gameQueryService.UpdateEntity(_currentGame);

        return true;
    }

    private async Task AbandonCurrentGame()
    {
        _currentGame!.State = State.ABANDONED;
        gameQueryService.UpdateEntity(_currentGame!);

        await StartFreshGame();
    }

    private async Task StartFreshGame()
    {
        var word = await wordService.GetRandomWord();
        _currentGame = new Game() {Word = word, AttemptsLeft = 6, State = State.ONGOING};
    }
}
