using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.Logging;
using Wordle.Abstraction.Enums;
using Wordle.Abstraction.Interfaces.Model.Entity;
using Wordle.Abstraction.Interfaces.Persistence;
using Wordle.Abstraction.Services;
using Wordle.Model.Entity;
using Wordle.Model.Searchable;

namespace Wordle.Services;

public class GameService : IGameService
{
    private const string ALPHABET = "abcdefghijklmnopqrstuvwxyz";
    private readonly IEntityQueryService<Game, SearchableGame> gameQueryService;
    private readonly IWordService wordService;
    private readonly IGuessService guessService;
    private readonly ILogger<GameService> logger;
    private static Game? _currentGame = null;

    public GameService(IEntityQueryService<Game, SearchableGame> gameQueryService, IWordService wordService, IGuessService guessService, ILogger<GameService> logger)
    {
        this.gameQueryService = gameQueryService;
        this.wordService = wordService;
        this.guessService = guessService;
        this.logger = logger;
    }

    public Task StartNewGame()
    {
        return _currentGame == null ? StartFreshGame() : AbandonCurrentGame();
    }

    /// <inheritdoc />
    public async Task<bool> ProcessGuess(string guessedWord)
    {
        try
        {
            // TODO: Change to new method that ensure cache has content.
            await wordService.GetRandomWord();

            if (_currentGame is null) throw new ArgumentNullException(nameof(_currentGame));
            if (await ShouldReturnEarly(guessedWord))
            {
                logger.LogInformation("The guess is invalid - returning early.");
                return false;
            }

            logger.LogInformation("The guess is valid - continuing...");
            var guess = guessService.ProcessGuess(_currentGame.Word, guessedWord, _currentGame.Guesses.Count);
            _currentGame.Guesses.Add(guess);
            _currentGame.AttemptsLeft--;

            if (guess.Word.Letters.All(x => x.CharacterState == CharacterState.CORRECT))
                _currentGame.GameState = GameState.WON;
            else if (_currentGame.AttemptsLeft <= 0)
                _currentGame.GameState = GameState.LOST;

            if (_currentGame.GameState == GameState.WON || _currentGame.GameState == GameState.LOST)
                logger.LogInformation("The current game has been {State} - the word was '{Word}'.",
                    _currentGame.GameState.ToString().ToLowerInvariant(), _currentGame.Word.Content);

            await gameQueryService.UpdateEntity(_currentGame);

            _currentGame.Letters = BuildAlphabetLettersFromGuesses(_currentGame);
            WeakReferenceMessenger.Default.Send(new GuessProcessedMessage(_currentGame, guess));
            WeakReferenceMessenger.Default.Send(new GameChangedMessage(_currentGame));

            return true;
        }
        catch (Exception e)
        {
            logger.LogError(e, $"Exception caught during execution of {nameof(ProcessGuess)}.");
            throw;
        }
    }

    private ICollection<ILetter> BuildAlphabetLettersFromGuesses(IGame game)
    {
        logger.LogInformation("Updating Games Letters information...");
        var bestByChar = new Dictionary<char, ILetter>(26);

        foreach (var letter in game.Guesses.SelectMany(g => g.Word.Letters))
        {
            var key = char.ToLowerInvariant(letter.Content);

            if (!bestByChar.TryGetValue(key, out var current) || letter.CharacterState > current.CharacterState)
            {
                bestByChar[key] = letter;
            }
        }

        var result = new List<ILetter>(ALPHABET.Length);
        result.AddRange(ALPHABET.Select(ch =>
            bestByChar.TryGetValue(ch, out var best)
                ? best
                : new Letter {Content = ch, CharacterState = CharacterState.UNKNOWN}));

        return result;
    }

    /// <inheritdoc />
    public async Task Initialize()
    {
        try
        {
            logger.LogInformation("Initializing Game Service...");
            if (_currentGame != null)
            {
                logger.LogWarning($"{nameof(Initialize)} called, while Current Game was already set.");
                return;
            }

            var games = (await gameQueryService.GetEntities(new SearchableGame() { GameState = GameState.ONGOING, })).ToList();
            if (games.Count >= 1)
            {
                logger.LogInformation("One or more ongoing games has been found - continuing the first one found.");
                _currentGame = games[0];
                WeakReferenceMessenger.Default.Send(new GameChangedMessage(_currentGame));
                return;
            }

            await StartNewGame();
        }
        catch (Exception e)
        {
            logger.LogError(e, $"Exception caught during execution of {nameof(Initialize)}.");
            throw;
        }
    }

    private async Task<bool> ShouldReturnEarly(string guessedWord)
    {
        logger.LogInformation("Checking if guessed word - {GuessedWord} - is a valid guess.", guessedWord);
        if (guessedWord.Length != _currentGame!.Letters.Count) return true;
        if (!await wordService.IsGuessedWordValid(guessedWord)) return true;
        return false;
    }

    private async Task AbandonCurrentGame()
    {
        logger.LogInformation("Abandoning the current game...");
        _currentGame!.GameState = GameState.ABANDONED;
        await gameQueryService.UpdateEntity(_currentGame!);

        await StartFreshGame();
    }

    private async Task StartFreshGame()
    {
        logger.LogInformation("Starting a new game...");
        var word = await wordService.GetRandomWord();
        _currentGame = new Game() {Word = word, AttemptsLeft = word.Letters.Count + 1, GameState = GameState.ONGOING,};
        _currentGame.Letters = BuildAlphabetLettersFromGuesses(_currentGame);

        await gameQueryService.AddEntity(_currentGame);

        WeakReferenceMessenger.Default.Send(new GameChangedMessage(_currentGame));
    }
}

public sealed record GameChangedMessage(IGame Game);

public sealed record GuessProcessedMessage(IGame Game, IGuess Guess);
