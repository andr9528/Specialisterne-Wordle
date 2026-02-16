using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Wordle.Abstraction.Enums;
using Wordle.Abstraction.Interfaces.Model.Entity;
using Wordle.Model.Entity;
using Wordle.Persistence;

namespace Wordle.Tests.Services.Persistence;

internal static class TestDb
{
    internal static (SqliteConnection Connection, WordleDatabaseContext Context) CreateContext()
    {
        var connection = new SqliteConnection("Filename=:memory:");
        connection.Open();

        var options = new DbContextOptionsBuilder<WordleDatabaseContext>()
            .UseSqlite(connection)
            .EnableSensitiveDataLogging()
            .Options;

        var context = new WordleDatabaseContext(options);
        context.Database.EnsureCreated();

        return (connection, context);
    }

    internal static void SeedBasicData(WordleDatabaseContext context)
    {
        var solutionTest = BuildWord("TEST");
        var guessNope = BuildWord("NOPE");
        var guessTest = BuildWord("TEST"); // duplicate content allowed
        var solutionAbcd = BuildWord("ABCD");

        Word letterTestWord = BuildLetterTestWord();
        (Game game1, Game game2) = BuildGameEntities(solutionTest, guessNope, solutionAbcd, guessTest);

        context.AddRange(game1, game2, letterTestWord);
        context.SaveChanges();
    }

    private static (Game game1, Game game2) BuildGameEntities(
        Word solutionTest, Word guessNope, Word solutionAbcd, Word guessTest)
    {
        var game1 = new Game
        {
            Word = solutionTest,
            AttemptsLeft = 3,
            GameState = GameState.ONGOING,
            Guesses = new List<IGuess>
            {
                new Guess
                {
                    Number = 1,
                    Word = guessNope
                }
            }
        };

        var game2 = new Game
        {
            Word = solutionAbcd,
            AttemptsLeft = 1,
            GameState = GameState.WON,
            Guesses = new List<IGuess>
            {
                new Guess
                {
                    Number = 1,
                    Word = guessTest
                }
            }
        };
        return (game1, game2);
    }

    private static Word BuildLetterTestWord()
    {
        var letterTestWord = new Word
        {
            Content = "AAB",
            Letters = new List<ILetter>()
        };

        letterTestWord.Letters.Add(new Letter
        {
            Content = 'a',
            Position = 1,
            CharacterState = CharacterState.ABSENT,
            Word = letterTestWord
        });

        letterTestWord.Letters.Add(new Letter
        {
            Content = 'A',
            Position = 2,
            CharacterState = CharacterState.CORRECT,
            Word = letterTestWord
        });

        letterTestWord.Letters.Add(new Letter
        {
            Content = 'b',
            Position = 1,
            CharacterState = CharacterState.PRESENT,
            Word = letterTestWord
        });
        return letterTestWord;
    }

    private static Word BuildWord(string content)
    {
        var word = new Word
        {
            Content = content,
            Letters = new List<ILetter>()
        };

        for (int i = 0; i < content.Length; i++)
        {
            var letter = new Letter
            {
                Content = content[i],
                Position = i,
                Word = word
            };

            word.Letters.Add(letter);
        }

        return word;
    }
}
