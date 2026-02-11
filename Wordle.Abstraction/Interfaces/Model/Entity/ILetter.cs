using Wordle.Abstraction.Interfaces.Model.Searchable;
using Wordle.Abstraction.Interfaces.Persistence;

namespace Wordle.Abstraction.Interfaces.Model.Entity;

public interface ILetter : ISearchableLetter, IEntity
{
    public IWord Word { get; set; }
}
