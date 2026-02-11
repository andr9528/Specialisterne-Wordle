using System.Text.Json.Serialization;
using Wordle.Abstraction.Interfaces.Model.Entity;

namespace Wordle.Model.Entity;

public class Letter : ILetter
{
    private readonly int id;

    /// <inheritdoc />
    public int Id
    {
        get => id;
        set => throw new InvalidOperationException(
            $"{nameof(Id)} cannot be changed after creation of {nameof(Letter)} entity");
    }

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

    public Letter()
    {
        
    }

    /// <summary>
    ///     Constructor for Entity Framework Core to use.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="word"></param>
    [JsonConstructor]
    private Letter(int id, Word word)
    {
        this.id = id;
        Word = word;
    }
}
