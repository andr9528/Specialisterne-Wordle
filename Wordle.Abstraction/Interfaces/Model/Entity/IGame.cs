using Wordle.Abstraction.Interfaces.Model.Searchable;
using Wordle.Abstraction.Interfaces.Persistence;

namespace Wordle.Abstraction.Interfaces.Model.Entity;

public interface IGame : ISearchableGame, IEntity
{
    IWord Word { get; set; }
    ICollection<IGuess> Guesses { get; set; }
    ICollection<ILetter> Letters { get; set; }
}
