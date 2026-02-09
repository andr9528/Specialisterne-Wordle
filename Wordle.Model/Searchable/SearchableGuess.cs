using Wordle.Abstraction.Interfaces.Model.Entity;
using Wordle.Abstraction.Interfaces.Model.Searchable;

namespace Wordle.Model.Searchable;

public class SearchableGuess : ISearchableGuess
{
    /// <inheritdoc />
    public int Id { get; set; }

    /// <inheritdoc />
    public int Number { get; set; }

    /// <inheritdoc />
    public int GameId { get; set; }

    /// <inheritdoc />
    public int WordId { get; set; }
}
