using Microsoft.Extensions.Logging;
using Moq;
using Wordle.Services;
using Path = System.IO.Path;

namespace Wordle.Tests.Services;

[TestFixture]
public sealed class FileWordServiceTests
{
    private string tempDir = null!;

    [SetUp]
    public void SetUp()
    {
        tempDir = Path.Combine(Path.GetTempPath(), "WordleTests_" + Guid.NewGuid());
        Directory.CreateDirectory(tempDir);
    }

    [TearDown]
    public void TearDown()
    {
        if (Directory.Exists(tempDir))
            Directory.Delete(tempDir, recursive: true);
    }

    [Test]
    public async Task GetWords_WhenFileDoesNotExist_ThrowsFileNotFoundException()
    {
        // Arrange
        var logger = Mock.Of<ILogger<FileWordService>>();
        var path = Path.Combine(tempDir, "missing.txt");
        var service = new FileWordService(logger, path);

        // Act
        Func<Task> act = async () => _ = await service.GetWords();

        // Assert
        await act.Should().ThrowAsync<FileNotFoundException>()
            .WithMessage($"*{path}*");
    }

    [Test]
    public async Task GetWords_WhenFileExists_LoadsWords_SkipsFirstLine_Trims_AndIgnoresEmpty()
    {
        // Arrange
        var logger = Mock.Of<ILogger<FileWordService>>();
        var path = Path.Combine(tempDir, "words.txt");

        // first line is skipped by implementation
        await File.WriteAllLinesAsync(path, new[]
        {
            "HEADER",
            "  alpha  ",
            "",
            "   ",
            "bravo",
            " charlie "
        });

        var service = new FileWordService(logger, path);

        // Act
        var words = await service.GetWords();

        // Assert
        words.Should().Equal("alpha", "bravo", "charlie");
    }

    [Test]
    public async Task GetWords_CachesResult_AndDoesNotReReadFile()
    {
        // Arrange
        var logger = Mock.Of<ILogger<FileWordService>>();
        var path = Path.Combine(tempDir, "words.txt");

        await File.WriteAllLinesAsync(path, new[]
        {
            "HEADER",
            "alpha",
        });

        var service = new FileWordService(logger, path);

        // Act
        var first = await service.GetWords();

        // Mutate file after first load
        await File.WriteAllLinesAsync(path, new[]
        {
            "HEADER",
            "alpha",
            "bravo",
        });

        var second = await service.GetWords();

        // Assert
        first.Should().BeSameAs(second);
        second.Should().Equal("alpha"); // proves cache
    }
}
