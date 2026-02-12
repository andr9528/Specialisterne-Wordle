using CommunityToolkit.Mvvm.Messaging;
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
    private static Game? _currentGame = null;

    public GameService(IEntityQueryService<Game, SearchableGame> gameQueryService, IWordService wordService, IGuessService guessService)
    {
        this.gameQueryService = gameQueryService;
        this.wordService = wordService;
        this.guessService = guessService;
    }

    public Task StartNewGame()
    {
        return _currentGame == null ? StartFreshGame() : AbandonCurrentGame();
    }

    /// <inheritdoc />
    public async Task<bool> ProcessGuess(string guessedWord)
    {
        if (_currentGame is null) throw new ArgumentNullException(nameof(_currentGame));
        if (await ShouldReturnEarly(guessedWord))
        {
            return false;
        }

        var guess = guessService.ProcessGuess(_currentGame.Word, guessedWord, _currentGame.Guesses.Count);
        _currentGame.Guesses.Add(guess);
        _currentGame.AttemptsLeft--;

        if (guess.Word.Letters.All(x => x.CharacterState == CharacterState.CORRECT))
            _currentGame.GameState = GameState.WON;
        else if (_currentGame.AttemptsLeft <= 0)
            _currentGame.GameState = GameState.LOST;

        await gameQueryService.UpdateEntity(_currentGame);

        _currentGame.Letters = BuildAlphabetLettersFromGuesses(_currentGame);
        WeakReferenceMessenger.Default.Send(new GuessProcessedMessage(_currentGame, guess));
        WeakReferenceMessenger.Default.Send(new GameChangedMessage(_currentGame));

        return true;
    }

    private ICollection<ILetter> BuildAlphabetLettersFromGuesses(IGame game)
    {
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
        if (_currentGame != null) return;

        var games = (await gameQueryService.GetEntities(new SearchableGame() {GameState = GameState.ONGOING,})).ToList();
        if (games.Count >= 1)
        {
            _currentGame = games[0];
            return;
        }

        await StartNewGame();
    }

    private async Task<bool> ShouldReturnEarly(string guessedWord)
    {
        if (guessedWord.Length != _currentGame!.Letters.Count) return true;
        if (!await wordService.IsGuessedWordValid(guessedWord)) return true;
        return false;
    }

    private async Task AbandonCurrentGame()
    {
        _currentGame!.GameState = GameState.ABANDONED;
        await gameQueryService.UpdateEntity(_currentGame!);

        await StartFreshGame();
    }

    private async Task StartFreshGame()
    {
        var word = await wordService.GetRandomWord();
        _currentGame = new Game() {Word = word, AttemptsLeft = word.Letters.Count + 1, GameState = GameState.ONGOING,};

        await gameQueryService.AddEntity(_currentGame);

        WeakReferenceMessenger.Default.Send(new GameChangedMessage(_currentGame));
    }
}

public sealed record GameChangedMessage(IGame Game);

public sealed record GuessProcessedMessage(IGame Game, IGuess Guess);
