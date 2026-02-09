using Wordle.Abstraction.Enums;
using Wordle.Abstraction.Interfaces.Model.Searchable;

namespace Wordle.Model.Searchable;

public class SearchableGame : ISearchableGame
{
    /// <inheritdoc />
    public int Id { get; set; }

    /// <inheritdoc />
    public int WordId { get; set; }

    /// <inheritdoc />
    public State State { get; set; }

    /// <inheritdoc />
    public int AttemptsLeft { get; set; }
}
