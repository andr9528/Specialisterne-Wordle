using Microsoft.Extensions.Logging;
using Moq;
using Wordle.Abstraction.Interfaces.Model.Entity;
using Wordle.Services;

namespace Wordle.Tests.Services;


[TestFixture]
public sealed class BaseWordServiceTests
{
    public class TestWordService : BaseWordService<TestWordService>
    {
        private readonly IList<string> wordsToReturn;
        public int LoadWordsCallCount { get; private set; }

        public TestWordService(ILogger<TestWordService> logger, IList<string> wordsToReturn) : base(logger)
        {
            this.wordsToReturn = wordsToReturn;
        }

        protected override Task<IList<string>> LoadWords()
        {
            LoadWordsCallCount++;
            return Task.FromResult(wordsToReturn);
        }
    }

    [Test]
    public async Task GetWords_FirstCall_LoadsWords()
    {
        // Arrange
        var logger = Mock.Of<ILogger<TestWordService>>();
        var service = new TestWordService(logger, new List<string> {"alpha", "bravo"});

        // Act
        var result = await service.GetWords();

        // Assert
        result.Should().BeEquivalentTo(new[] {"alpha", "bravo"});
        service.LoadWordsCallCount.Should().Be(1);
    }

    [Test]
    public async Task GetWords_SecondCall_ReturnsCachedList_AndDoesNotReload()
    {
        // Arrange
        var logger = Mock.Of<ILogger<TestWordService>>();
        var service = new TestWordService(logger, new List<string> {"alpha", "bravo"});

        // Act
        var first = await service.GetWords();
        var second = await service.GetWords();

        // Assert
        first.Should().BeSameAs(second);
        service.LoadWordsCallCount.Should().Be(1);
    }

    [Test]
    public async Task GetRandomWord_WhenSingleWordExists_ReturnsThatWordWithLetters()
    {
        // Arrange
        var logger = Mock.Of<ILogger<TestWordService>>();
        var service = new TestWordService(logger, new List<string> {"test"});

        // Act
        IWord word = await service.GetRandomWord();

        // Assert
        word.Content.Should().Be("test");
        word.Letters.Should().HaveCount(4);
        word.Letters.Should().SatisfyRespectively(l => l.Content.Should().Be('t'), l => l.Content.Should().Be('e'),
            l => l.Content.Should().Be('s'), l => l.Content.Should().Be('t'));
    }

    [Test]
    public async Task IsGuessedWordValid_WhenWordExists_ReturnsTrue()
    {
        // Arrange
        var logger = Mock.Of<ILogger<TestWordService>>();
        var service = new TestWordService(logger, new List<string> {"alpha", "bravo"});

        // Act
        var result = await service.IsGuessedWordValid("bravo");

        // Assert
        result.Should().BeTrue();
    }

    [Test]
    public async Task IsGuessedWordValid_WhenWordDoesNotExist_ReturnsFalse()
    {
        // Arrange
        var logger = Mock.Of<ILogger<TestWordService>>();
        var service = new TestWordService(logger, new List<string> {"alpha", "bravo"});

        // Act
        var result = await service.IsGuessedWordValid("charlie");

        // Assert
        result.Should().BeFalse();
    }
}

