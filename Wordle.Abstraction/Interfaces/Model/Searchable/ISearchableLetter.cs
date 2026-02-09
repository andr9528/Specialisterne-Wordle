using Wordle.Abstraction.Interfaces.Persistence;

namespace Wordle.Abstraction.Interfaces.Model.Searchable;

public interface ISearchableLetter : ISearchable
{
    public int WordId { get; set; }
    public char Content { get; set; }
    public int Position { get; set; }
}
