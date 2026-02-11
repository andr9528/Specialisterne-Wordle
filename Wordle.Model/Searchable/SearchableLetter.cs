using Wordle.Abstraction.Enums;
using Wordle.Abstraction.Interfaces.Model.Searchable;

namespace Wordle.Model.Searchable;

public class SearchableLetter : ISearchableLetter
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
    public CharacterState CharacterState { get; set; }
}
