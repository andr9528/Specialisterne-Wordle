using Wordle.Abstraction.Interfaces.Persistence;

namespace Wordle.Abstraction.Interfaces.Model.Searchable;

public interface ISearchableGuess : ISearchable
{
    public int Number { get; set; }
    public int GameId { get; set; }
    public int WordId { get; set; }
}
