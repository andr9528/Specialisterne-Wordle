using Wordle.Abstraction.Interfaces.Model.Searchable;
using Wordle.Abstraction.Interfaces.Persistence;

namespace Wordle.Abstraction.Interfaces.Model.Entity;

public interface ILetter : ISearchableLetter, IEntity
{
    public bool IncludedInWord { get; set; }

    /// <summary>
    /// Is true, If the letter in question is in the word, and correct spot.
    /// </summary>
    public bool CorrectPosition { get; set; }
    public IWord Word { get; set; }
}
