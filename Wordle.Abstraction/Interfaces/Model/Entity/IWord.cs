using Wordle.Abstraction.Interfaces.Model.Searchable;
using Wordle.Abstraction.Interfaces.Persistence;

namespace Wordle.Abstraction.Interfaces.Model.Entity;

public interface IWord : ISearchableWord, IEntity
{
    ICollection<ILetter> Letters { get; set; }
}
