using System.Text.Json.Serialization;
using Wordle.Abstraction.Enums;
using Wordle.Abstraction.Interfaces.Model.Entity;
using Wordle.Abstraction.Interfaces.Model.Searchable;

namespace Wordle.Model.Entity;

public class Game : IGame
{
    private readonly int id;

    /// <inheritdoc />
    public int Id
    {
        get => id;
        set => throw new InvalidOperationException(
            $"{nameof(Id)} cannot be changed after creation of {nameof(Game)} entity");
    }

    /// <inheritdoc />
    public int WordId { get; set; }

    /// <inheritdoc />
    public GameState GameState { get; set; }

    /// <inheritdoc />
    public int AttemptsLeft { get; set; }

    /// <inheritdoc />
    public byte[] Version { get; set; }

    /// <inheritdoc />
    public DateTime CreatedDateTime { get; set; }

    /// <inheritdoc />
    public DateTime UpdatedDateTime { get; set; }

    /// <inheritdoc />
    public IWord Word { get; set; }

    /// <inheritdoc />
    public ICollection<IGuess> Guesses { get; set; } = new List<IGuess>();

    /// <inheritdoc />
    public ICollection<ILetter> Letters { get; set; } = new List<ILetter>();

    public Game()
    {
        
    }

    /// <summary>
    ///     Constructor for Entity Framework Core to use.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="word"></param>
    /// <param name="guesses"></param>
    [JsonConstructor]
    private Game(int id, Word word, List<Guess> guesses)
    {
        this.id = id;
        Word = word;
        Guesses = guesses.Cast<IGuess>().ToList();
        Letters = new List<ILetter>();
    }
}
