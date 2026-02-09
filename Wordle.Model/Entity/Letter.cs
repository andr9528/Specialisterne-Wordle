using Wordle.Abstraction.Interfaces.Model.Entity;

namespace Wordle.Model.Entity;

public class Letter : ILetter
{
    /// <inheritdoc />
    public int Id { get; set; }

    /// <inheritdoc />
    public int WordId { get; set; }

    /// <inheritdoc />
    public char Content { get; set; }

    /// <inheritdoc />
    public int Position { get; set; }

    /// <inheritdoc />
    public byte[] Version { get; set; }

    /// <inheritdoc />
    public DateTime CreatedDateTime { get; set; }

    /// <inheritdoc />
    public DateTime UpdatedDateTime { get; set; }

    /// <inheritdoc />
    public bool IncludedInWord { get; set; }

    /// <inheritdoc />
    public bool CorrectPosition { get; set; }

    /// <inheritdoc />
    public IWord Word { get; set; }
}
