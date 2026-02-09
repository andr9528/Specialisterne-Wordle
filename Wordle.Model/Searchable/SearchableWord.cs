using Wordle.Abstraction.Interfaces.Model.Searchable;

namespace Wordle.Model.Searchable;

public class SearchableWord : ISearchableWord
{
    /// <inheritdoc />
    public int Id { get; set; }

    /// <inheritdoc />
    public string Content { get; set; }
}
