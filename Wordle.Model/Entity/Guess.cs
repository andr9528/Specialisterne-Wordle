using System.Text.Json.Serialization;
using Wordle.Abstraction.Interfaces.Model.Entity;

namespace Wordle.Model.Entity;

public class Guess : IGuess
{
    private readonly int id;

    /// <inheritdoc />
    public int Id
    {
        get => id;
        set => throw new InvalidOperationException(
            $"{nameof(Id)} cannot be changed after creation of {nameof(Guess)} entity");
    }

    /// <inheritdoc />
    public int Number { get; set; }

    /// <inheritdoc />
    public int GameId { get; set; }

    /// <inheritdoc />
    public int WordId { get; set; }

    /// <inheritdoc />
    public byte[] Version { get; set; }

    /// <inheritdoc />
    public DateTime CreatedDateTime { get; set; }

    /// <inheritdoc />
    public DateTime UpdatedDateTime { get; set; }

    /// <inheritdoc />
    public IWord Word { get; set; }

    /// <inheritdoc />
    public IGame Game { get; set; }

    public Guess()
    {
        
    }

    /// <summary>
    ///     Constructor for Entity Framework Core to use.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="word"></param>
    /// <param name="game"></param>
    [JsonConstructor]
    private Guess(int id, Word word, Game game)
    {
        this.id = id;
        Word = word;
        Game = game;
    }
}
