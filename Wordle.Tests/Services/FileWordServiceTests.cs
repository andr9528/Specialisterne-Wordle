using System.Reflection;
using Microsoft.Extensions.Logging;
using Moq;
using Wordle.Services;
using Path = System.IO.Path;

namespace Wordle.Tests.Services;

[TestFixture]
public sealed class FileWordServiceTests
{
    private string FindEmbeddedWordListResourceName(Assembly assembly)
    {
        const string fileName = "wordle_ord.txt";

        var matches = assembly.GetManifestResourceNames()
            .Where(n => n.EndsWith(fileName, StringComparison.OrdinalIgnoreCase)).ToArray();

        matches.Should().HaveCount(1, "there must be exactly one embedded word list resource");
        return matches[0];
    }

    [Test]
    public async Task GetWords_LoadsWords_FromEmbeddedResource_SkipsFirstLine_Trims_AndIgnoresEmpty()
    {
        // Arrange
        var logger = Mock.Of<ILogger<FileWordService>>();
        var service = new FileWordService(logger);

        // Act
        var actual = await service.GetWords();

        // Assert
        // Build the expected output by reading the embedded resource directly.
        var asm = typeof(FileWordService).Assembly;
        var resourceName = FindEmbeddedWordListResourceName(asm);
        await using var stream = asm.GetManifestResourceStream(resourceName);
        stream.Should().NotBeNull($"Expected embedded resource '{resourceName}' to exist in {asm.FullName}");

        using var reader = new StreamReader(stream!);
        var rawLines = new List<string>();
        while (!reader.EndOfStream)
        {
            var line = await reader.ReadLineAsync();
            if (string.IsNullOrWhiteSpace(line))
                continue;

            rawLines.Add(line.Trim());
        }

        var expected = rawLines
            .Where(l => !string.IsNullOrWhiteSpace(l))
            .Skip(1) // first line is skipped by implementation
            .Select(l => l.Trim())
            .ToList();

        actual.Should().Equal(expected);
    }

    [Test]
    public async Task GetWords_CachesResult_AndDoesNotReReadEmbeddedResource()
    {
        // Arrange
        var logger = Mock.Of<ILogger<FileWordService>>();
        var service = new FileWordService(logger);

        // Act
        var first = await service.GetWords();
        var second = await service.GetWords();

        // Assert
        first.Should().BeSameAs(second);
    }

    [Test]
    public async Task GetWords_LogsLoading_AndLoadedCount()
    {
        // Arrange
        var logger = new Mock<ILogger<FileWordService>>();
        var service = new FileWordService(logger.Object);

        // Act
        var words = await service.GetWords();

        // Assert
        logger.Verify(
            x => x.Log(
                LogLevel.Information,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, _) => v.ToString()!.Contains("Loading words from embedded file")),
                It.IsAny<Exception?>(),
                It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
            Times.Once);

        logger.Verify(
            x => x.Log(
                LogLevel.Information,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, _) => v.ToString()!.Contains($"Loaded {words.Count} words into cache.")),
                It.IsAny<Exception?>(),
                It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
            Times.Once);
    }
}
