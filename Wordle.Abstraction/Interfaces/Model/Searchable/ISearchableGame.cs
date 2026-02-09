using Wordle.Abstraction.Enums;
using Wordle.Abstraction.Interfaces.Persistence;

namespace Wordle.Abstraction.Interfaces.Model.Searchable;

public interface ISearchableGame : ISearchable
{
    public int WordId { get; set; }
    public State State { get; set; }
    public int AttemptsLeft { get; set; }
}
