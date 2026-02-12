using Wordle.Abstraction.Interfaces.Persistence;

namespace Wordle.Abstraction.Interfaces.Model.Searchable;

public interface ISearchableGuess : ISearchable
{
    /// <summary>
    /// 1-indexed number for the Guess.
    /// </summary>
    public int Number { get; set; }
    public int GameId { get; set; }
    public int WordId { get; set; }
}
