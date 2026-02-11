using System.Text.Json.Serialization;
using Wordle.Abstraction.Interfaces.Model.Entity;

namespace Wordle.Model.Entity;

public class Word : IWord
{
    private readonly int id;

    /// <inheritdoc />
    public int Id
    {
        get => id;
        set => throw new InvalidOperationException(
            $"{nameof(Id)} cannot be changed after creation of {nameof(Word)} entity");
    }

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

    public Word()
    {
        
    }

    /// <summary>
    ///     Constructor for Entity Framework Core to use.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="letters"></param>
    [JsonConstructor]
    private Word(int id, List<Letter> letters)
    {
        this.id = id;
        Letters = letters.Cast<ILetter>().ToList();
    }
}
