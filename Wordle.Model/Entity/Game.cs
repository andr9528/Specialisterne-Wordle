using Wordle.Abstraction.Enums;
using Wordle.Abstraction.Interfaces.Model.Entity;
using Wordle.Abstraction.Interfaces.Model.Searchable;

namespace Wordle.Model.Entity;

public class Game : IGame
{
    /// <inheritdoc />
    public int Id { get; set; }

    /// <inheritdoc />
    public int WordId { get; set; }

    /// <inheritdoc />
    public State State { get; set; }

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
    public ICollection<IGuess> Guesses { get; set; }
}
