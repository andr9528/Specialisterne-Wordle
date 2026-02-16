using Wordle.Model.Searchable;
using Wordle.Persistence.Services;

namespace Wordle.Tests.Services.Persistence;

[TestFixture]
public sealed class WordQueryServiceTests
{
    [Test]
    public async Task GetEntities_WhenContentMatches_CaseInsensitive_ReturnsWord()
    {
        // Arrange
        var (conn, ctx) = TestDb.CreateContext();
        await using var _ = ctx;
        await using var __ = conn;

        TestDb.SeedBasicData(ctx);
        var sut = new WordQueryService(ctx);

        // Act
        var results = (await sut.GetEntities(new SearchableWord {Content = "test"})).ToList();

        // Assert
        results.Should().NotBeEmpty();
        results.Should().OnlyContain(w => w.Content.ToUpperInvariant() == "TEST");
    }

    [Test]
    public async Task GetEntities_IncludesLetters()
    {
        // Arrange
        var (conn, ctx) = TestDb.CreateContext();
        await using var _ = ctx;
        await using var __ = conn;

        TestDb.SeedBasicData(ctx);
        var sut = new WordQueryService(ctx);

        // Act
        var word = (await sut.GetEntities(new SearchableWord {Content = "TEST"})).First();

        // Assert
        word.Letters.Should().NotBeNull();
        word.Letters.Should().NotBeEmpty();
    }
}
