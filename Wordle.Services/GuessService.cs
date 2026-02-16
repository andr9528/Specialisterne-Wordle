using Microsoft.Extensions.Logging;
using Wordle.Abstraction.Enums;
using Wordle.Abstraction.Interfaces.Model.Entity;
using Wordle.Abstraction.Interfaces.Persistence;
using Wordle.Abstraction.Services;
using Wordle.Model.Entity;
using Wordle.Model.Searchable;

namespace Wordle.Services;

// TODO: Add Logging for Guessed letter, in position X existing or not and correct/incorrect position.
// TODO: Add Logging for Guessed Word.
public class GuessService : IGuessService
{
    private readonly ILogger<GuessService> logger;

    public GuessService(ILogger<GuessService> logger)
    {
        this.logger = logger;
    }

    /// <inheritdoc />
    public IGuess ProcessGuess(IWord currentGameWord, string guessedWord, int currentGuessCount)
    {
        logger.LogInformation($"Starting processing of guessed word - '{guessedWord}'");
        var letters = BuildLetters(guessedWord, currentGameWord);
        var word = new Word() {Content = guessedWord, Letters = letters.Cast<ILetter>().ToList()};
        return new Guess() {Word = word, Number = currentGuessCount + 1,};
    }

    private List<Letter> BuildLetters(string guessedWord, IWord word)
    {
        var letters = new List<Letter>();
        var secretLetters = word.Letters.ToList();

        for (int i = 0; i < secretLetters.Count; i++)
        {
            var secretLetter = secretLetters[i];
            var guessedLetter = guessedWord[i];

            var letter = new Letter() {Content = guessedLetter, Position = i, CharacterState = CharacterState.ABSENT,};

            if (secretLetters.Select(x => x.Content).Contains(guessedLetter))
                letter.CharacterState = CharacterState.PRESENT;
            if (secretLetter.Content == guessedLetter)
                letter.CharacterState = CharacterState.CORRECT;

            logger.LogInformation("Letter '{GuessedLetter}' in position '{Position}' is '{State}'.", guessedLetter,
                i + 1, letter.CharacterState.ToString());

            letters.Add(letter);
        }

        return letters;
    }
}
