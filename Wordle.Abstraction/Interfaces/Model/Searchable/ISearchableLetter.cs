using Wordle.Abstraction.Enums;
using Wordle.Abstraction.Interfaces.Persistence;

namespace Wordle.Abstraction.Interfaces.Model.Searchable;

public interface ISearchableLetter : ISearchable
{
    public int WordId { get; set; }
    public char Content { get; set; }
    /// <summary>
    /// 0-indexed position of a letter in a guess.
    /// </summary>
    public int Position { get; set; }
    public CharacterState CharacterState { get; set; }
}
