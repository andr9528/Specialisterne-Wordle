using Wordle.Abstraction.Interfaces.Model.Searchable;
using Wordle.Abstraction.Interfaces.Persistence;

namespace Wordle.Abstraction.Interfaces.Model.Entity;

public interface IGuess : ISearchableGuess, IEntity
{
    IWord Word { get; set; }
    IGame Game { get; set; }
}
