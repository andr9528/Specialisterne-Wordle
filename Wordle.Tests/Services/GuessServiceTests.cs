using Microsoft.Extensions.Logging;
using Moq;
using Wordle.Abstraction.Enums;
using Wordle.Abstraction.Interfaces.Model.Entity;
using Wordle.Model.Entity;
using Wordle.Services;

namespace Wordle.Tests.Services;

[TestFixture]
public sealed class GuessServiceTests
{
    [Test]
    public void ProcessGuess_SetsGuessNumberAndWordContentAndLetters()
    {
        // Arrange
        var logger = Mock.Of<ILogger<GuessService>>();
        var service = new GuessService(logger);

        var secret = new Word
        {
            Content = "abcd",
            Letters = new List<ILetter> {
            
                new Letter {Content = 'a', Position = 0},
                new Letter {Content = 'b', Position = 1},
                new Letter {Content = 'c', Position = 2},
                new Letter {Content = 'd', Position = 3},
            }
        };

        // Act
        var guess = service.ProcessGuess(secret, "abzz", currentGuessCount: 0);
        var letter = guess.Word.Letters.ToList();

        // Assert
        guess.Number.Should().Be(1);
        guess.Word.Content.Should().Be("abzz");
        guess.Word.Letters.Should().HaveCount(4);

        // a correct, b correct, z absent, z absent
        letter[0].Content.Should().Be('a');
        letter[0].CharacterState.Should().Be(CharacterState.CORRECT);

        letter[1].Content.Should().Be('b');
        letter[1].CharacterState.Should().Be(CharacterState.CORRECT);

        letter[2].Content.Should().Be('z');
        letter[2].CharacterState.Should().Be(CharacterState.ABSENT);

        letter[3].Content.Should().Be('z');
        letter[3].CharacterState.Should().Be(CharacterState.ABSENT);
    }

    [Test]
    public void ProcessGuess_MarksPresentWhenLetterExistsButWrongPosition()
    {
        // Arrange
        var logger = Mock.Of<ILogger<GuessService>>();
        var service = new GuessService(logger);

        var secret = new Word
        {
            Content = "abcd",
            Letters = new List<ILetter>
            {
                new Letter { Content = 'a', Position = 0 },
                new Letter { Content = 'b', Position = 1 },
                new Letter { Content = 'c', Position = 2 },
                new Letter { Content = 'd', Position = 3 },
            }
        };

        // Act
        var guess = service.ProcessGuess(secret, "baaa", currentGuessCount: 2);
        var letter = guess.Word.Letters.ToList();

        // Assert
        guess.Number.Should().Be(3);
        letter[0].CharacterState.Should().Be(CharacterState.PRESENT); // b exists but wrong spot
        letter[1].CharacterState.Should().Be(CharacterState.PRESENT); // a exists but wrong spot (since secret at 0)
    }
}
