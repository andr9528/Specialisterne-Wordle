using Wordle.Abstraction.Interfaces.Persistence;

namespace Wordle.Abstraction.Interfaces.Model.Searchable;

public interface ISearchableWord : ISearchable
{
    string Content { get; set; }
}
