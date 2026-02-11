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
    public GuessService()
    {
    }

    /// <inheritdoc />
    public IGuess ProcessGuess(IGame currentGame, string guessedWord)
    {
        if (currentGame is not Game game)
            throw new ArgumentException(
                $"Expected {nameof(currentGame)} to be a {nameof(Game)} but was {currentGame.GetType().Name}.",
                nameof(currentGame));

        var letters = BuildLetters(guessedWord, game);
        var word = new Word() {Content = guessedWord, Letters = letters.Cast<ILetter>().ToList()};
        return new Guess() {Word = word, Number = currentGame.Guesses.Count + 1,};
    }

    private List<Letter> BuildLetters(string guessedWord, Game game)
    {
        var letters = new List<Letter>();
        var secretLetters = game.Word.Letters.ToList();

        for (int i = 0; i < secretLetters.Count; i++)
        {
            var secretLetter = secretLetters[i];
            var guessedLetter = guessedWord[i];

            var letter = new Letter() {Content = guessedLetter, Position = i,};

            if (secretLetters.Select(x => x.Content).Contains(guessedLetter))
                letter.IncludedInWord = true;
            if (secretLetter.Content == guessedLetter)
                letter.CorrectPosition = true;

            letters.Add(letter);
        }

        return letters;
    }
}
