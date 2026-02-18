using System.Reflection;
using Microsoft.Extensions.Logging;
using Moq;
using Wordle.Abstraction.Enums;
using Wordle.Abstraction.Interfaces.Model.Entity;
using Wordle.Abstraction.Interfaces.Persistence;
using Wordle.Abstraction.Services;
using Wordle.Model.Entity;
using Wordle.Model.Searchable;
using Wordle.Services;

namespace Wordle.Tests.Services;

[TestFixture]
public sealed class GameServiceTests
{
    private Mock<IEntityQueryService<Game, SearchableGame>> gameQuery = null!;
    private Mock<IWordService> wordService = null!;
    private Mock<IGuessService> guessService = null!;
    private ILogger<GameService> logger = null!;

    [SetUp]
    public void SetUp()
    {
        gameQuery = new Mock<IEntityQueryService<Game, SearchableGame>>();
        wordService = new Mock<IWordService>();
        guessService = new Mock<IGuessService>();
        logger = Mock.Of<ILogger<GameService>>();
    }

    [Test]
    public async Task StartNewGame_WhenNoCurrentGame_StartsFreshAndAddsEntity()
    {
        // Arrange
        var word = BuildWord("test");
        wordService.Setup(x => x.GetRandomWord()).ReturnsAsync(word);

        gameQuery.Setup(x => x.AddEntity(It.IsAny<Game>()))
            .Returns(Task.CompletedTask);

        var sut = CreateSut();

        // Act
        await sut.StartNewGame();

        // Assert
        gameQuery.Verify(x => x.AddEntity(It.Is<Game>(g =>
            g.Word.Content == "test" &&
            g.AttemptsLeft == word.Letters.Count + 1 &&
            g.GameState == GameState.ONGOING
        )), Times.Once);

        gameQuery.VerifyNoOtherCalls();
        wordService.VerifyAll();
        guessService.VerifyNoOtherCalls();
    }

    [Test]
    public async Task StartNewGame_WhenCurrentGameExists_AbandonsAndStartsNew()
    {
        // Arrange
        var existing = new Game
        {
            Word = BuildWord("aaaa"),
            AttemptsLeft = 2,
            GameState = GameState.ONGOING,
            Guesses = new List<IGuess>()
        };

        var sut = CreateSut();
        SetCurrentGame(sut, existing);

        gameQuery.Setup(x => x.UpdateEntity(It.IsAny<Game>())).Returns(Task.CompletedTask);

        var newWord = BuildWord("test");
        wordService.Setup(x => x.GetRandomWord()).ReturnsAsync(newWord);

        gameQuery.Setup(x => x.AddEntity(It.IsAny<Game>()))
            .Returns(Task.CompletedTask);

        // Act
        await sut.StartNewGame();

        // Assert
        gameQuery.Verify(x => x.UpdateEntity(It.Is<Game>(g => g.GameState == GameState.ABANDONED)), Times.Once);
        gameQuery.Verify(x => x.AddEntity(It.Is<Game>(g => g.Word.Content == "test" && g.GameState == GameState.ONGOING)), Times.Once);

        gameQuery.VerifyNoOtherCalls();
        wordService.VerifyAll();
        guessService.VerifyNoOtherCalls();
    }

    [Test]
    public async Task Initialize_WhenOngoingGameExists_SetsCurrentGame_AndDoesNotStartNew()
    {
        // Arrange
        var existing = new Game
        {
            Word = BuildWord("test"),
            AttemptsLeft = 3,
            GameState = GameState.ONGOING,
            Guesses = new List<IGuess>()
        };

        gameQuery.Setup(x => x.GetEntities(It.Is<SearchableGame>(s => s.GameState == GameState.ONGOING)))
            .ReturnsAsync(new List<Game> { existing });

        var sut = CreateSut();

        // Act
        await sut.Initialize();

        // Assert
        GetCurrentGame(sut).Should().NotBeNull();
        GetCurrentGame(sut)!.GameState.Should().Be(GameState.ONGOING);
        GetCurrentGame(sut)!.Word.Content.Should().Be("test");

        gameQuery.VerifyAll();
        wordService.VerifyNoOtherCalls();
        guessService.VerifyNoOtherCalls();
    }

    [Test]
    public async Task Initialize_WhenNoOngoingGames_StartsNewGame()
    {
        // Arrange
        gameQuery.Setup(x => x.GetEntities(It.IsAny<SearchableGame>()))
            .ReturnsAsync(new List<Game>());

        var word = BuildWord("test");
        wordService.Setup(x => x.GetRandomWord()).ReturnsAsync(word);

        gameQuery.Setup(x => x.AddEntity(It.IsAny<Game>()))
            .Returns(Task.CompletedTask);

        var sut = CreateSut();

        // Act
        await sut.Initialize();

        // Assert
        gameQuery.Verify(x => x.AddEntity(It.IsAny<Game>()), Times.Once);
        wordService.VerifyAll();
        guessService.VerifyNoOtherCalls();
    }

    [Test]
    public async Task ProcessGuess_WhenGuessLengthIsWrong_ReturnsFalse_AndDoesNotUpdateEntity()
    {
        // Arrange
        var current = new Game
        {
            Word = BuildWord("test"),
            AttemptsLeft = 3,
            GameState = GameState.ONGOING,
            Guesses = new List<IGuess>()
        };

        var sut = CreateSut();
        SetCurrentGame(sut, current);

        // Act
        var result = await sut.ProcessGuess("nope!!"); // wrong length (6 vs 4)

        // Assert
        result.Should().BeFalse();
        gameQuery.Verify(x => x.UpdateEntity(It.IsAny<Game>()), Times.Never);
        guessService.Verify(x => x.ProcessGuess(It.IsAny<Word>(), It.IsAny<string>(), It.IsAny<int>()), Times.Never);

        wordService.VerifyNoOtherCalls(); // dictionary check shouldn't run due to length mismatch
    }

    [Test]
    public async Task ProcessGuess_WhenWordNotInDictionary_ReturnsFalse_AndDoesNotUpdateEntity()
    {
        // Arrange
        var current = new Game
        {
            Word = BuildWord("test"),
            AttemptsLeft = 3,
            GameState = GameState.ONGOING,
            Guesses = new List<IGuess>()
        };

        var sut = CreateSut();
        SetCurrentGame(sut, current);

        wordService.Setup(x => x.IsGuessedWordValid("nope")).ReturnsAsync(false);

        // Act
        var result = await sut.ProcessGuess("nope");

        // Assert
        result.Should().BeFalse();
        gameQuery.Verify(x => x.UpdateEntity(It.IsAny<Game>()), Times.Never);
        guessService.Verify(x => x.ProcessGuess(It.IsAny<Word>(), It.IsAny<string>(), It.IsAny<int>()), Times.Never);

        wordService.VerifyAll();
    }

    [Test]
    public async Task ProcessGuess_WhenValidGuess_AddsGuess_DecrementsAttempts_AndUpdatesEntity()
    {
        // Arrange
        var current = new Game
        {
            Word = BuildWord("test"),
            AttemptsLeft = 3,
            GameState = GameState.ONGOING,
            Guesses = new List<IGuess>()
        };

        var sut = CreateSut();
        SetCurrentGame(sut, current);

        wordService.Setup(x => x.IsGuessedWordValid("nope")).ReturnsAsync(true);

        var processedGuess = new Guess
        {
            Number = 1,
            Word = BuildWord("nope")
        };

        guessService.Setup(x => x.ProcessGuess(current.Word, "nope", 0))
            .Returns(processedGuess);

        gameQuery.Setup(x => x.UpdateEntity(It.IsAny<Game>()))
            .Returns(Task.CompletedTask);

        // Act
        var result = await sut.ProcessGuess("nope");

        // Assert
        result.Should().BeTrue();

        current.Guesses.Should().HaveCount(1);
        current.AttemptsLeft.Should().Be(2);

        gameQuery.Verify(x => x.UpdateEntity(It.Is<Game>(g => g.Guesses.Count == 1 && g.AttemptsLeft == 2)), Times.Once);
        wordService.VerifyAll();
        guessService.VerifyAll();
    }

    [Test]
    public async Task ProcessGuess_WhenAllLettersCorrect_SetsGameStateWon()
    {
        // Arrange
        var current = new Game
        {
            Word = BuildWord("test"),
            AttemptsLeft = 2,
            GameState = GameState.ONGOING,
            Guesses = new List<IGuess>()
        };

        var sut = CreateSut();
        SetCurrentGame(sut, current);

        wordService.Setup(x => x.IsGuessedWordValid("test")).ReturnsAsync(true);
        wordService.Setup(x => x.GetRandomWord()).ReturnsAsync(BuildWord("new"));

        var winningGuess = new Guess
        {
            Number = 1,
            Word = new Word
            {
                Content = "test",
                Letters = new List<ILetter>
                {
                    new Letter { Content = 't', Position = 0, CharacterState = CharacterState.CORRECT },
                    new Letter { Content = 'e', Position = 1, CharacterState = CharacterState.CORRECT },
                    new Letter { Content = 's', Position = 2, CharacterState = CharacterState.CORRECT },
                    new Letter { Content = 't', Position = 3, CharacterState = CharacterState.CORRECT }
                }
            }
        };

        guessService.Setup(x => x.ProcessGuess(current.Word, "test", 0)).Returns(winningGuess);

        gameQuery.Setup(x => x.UpdateEntity(It.IsAny<Game>())).Returns(Task.CompletedTask);

        // Act
        var result = await sut.ProcessGuess("test");

        // Assert
        result.Should().BeTrue();
        current.GameState.Should().Be(GameState.WON);
    }

    // ---------- Helpers ----------

    private GameService CreateSut()
        => new(gameQuery.Object, wordService.Object, guessService.Object, logger);

    private static Word BuildWord(string content)
    {
        var letters = content.Select((ch, i) =>
            (ILetter)new Letter
            {
                Content = ch,
                Position = i
            }).ToList();

        return new Word { Content = content, Letters = letters };
    }

    private static void SetCurrentGame(GameService sut, Game game)
    {
        var field = typeof(GameService).GetField("_currentGame", BindingFlags.NonPublic | BindingFlags.Instance);
        field.Should().NotBeNull("GameService should have a private instance field named _currentGame");
        field!.SetValue(sut, game);
    }

    private static Game? GetCurrentGame(GameService sut)
    {
        var field = typeof(GameService).GetField("_currentGame", BindingFlags.NonPublic | BindingFlags.Instance);
        field.Should().NotBeNull("GameService should have a private instance field named _currentGame");
        return (Game?)field!.GetValue(sut);
    }
}
