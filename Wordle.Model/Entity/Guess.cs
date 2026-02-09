using Wordle.Abstraction.Interfaces.Model.Entity;

namespace Wordle.Model.Entity;

public class Guess : IGuess
{
    /// <inheritdoc />
    public int Id { get; set; }

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
}
