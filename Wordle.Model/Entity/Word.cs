using Wordle.Abstraction.Interfaces.Model.Entity;

namespace Wordle.Model.Entity;

public class Word : IWord
{
    /// <inheritdoc />
    public int Id { get; set; }

    /// <inheritdoc />
    public string Content { get; set; }

    /// <inheritdoc />
    public byte[] Version { get; set; }

    /// <inheritdoc />
    public DateTime CreatedDateTime { get; set; }

    /// <inheritdoc />
    public DateTime UpdatedDateTime { get; set; }

    /// <inheritdoc />
    public ICollection<ILetter> Letters { get; set; }
}
