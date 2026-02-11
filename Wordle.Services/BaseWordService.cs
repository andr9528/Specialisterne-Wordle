using Wordle.Abstraction.Enums;
using Wordle.Abstraction.Interfaces.Model.Entity;
using Wordle.Abstraction.Services;
using Wordle.Model.Entity;

namespace Wordle.Services;

public abstract class BaseWordService : IWordService
{
    private IList<string>? _words = null;

    protected abstract Task<IList<string>> LoadWords();

    /// <inheritdoc />
    public async Task<IList<string>> GetWords()
    {
        if (_words != null)
        {
            return _words;
        }

        _words = await LoadWords();
        return _words;
    }

    /// <inheritdoc />
    public async Task<IWord> GetRandomWord()
    {
        var words = await GetWords();

        var rawWord = words[Random.Shared.Next(words.Count)];

        var letters = ToLettersList(rawWord);
        var word = new Word() {Letters = letters, Content = rawWord,};

        return word;
    }

    /// <inheritdoc />
    public async Task<bool> IsGuessedWordValid(string guess)
    {
        return (await GetWords()).Contains(guess);
    }

    private List<ILetter> ToLettersList(string text)
    {
        return text.Select((x, pos) => new Letter() {
            Content = x,
            Position = pos,
            CharacterState = CharacterState.PRESENT,
        }).Cast<ILetter>().ToList();
    }
}
