using Wordle.Model.Searchable;
using Wordle.Persistence.Services;

namespace Wordle.Tests.Services.Persistence;

[TestFixture]
public sealed class GuessQueryServiceTests
{
    [Test]
    public async Task GetEntities_WhenFilteringByNumber_ReturnsMatches()
    {
        // Arrange
        var (conn, ctx) = TestDb.CreateContext();
        await using var _ = ctx;
        await using var __ = conn;

        TestDb.SeedBasicData(ctx);
        var sut = new GuessQueryService(ctx);

        // Act
        var results = (await sut.GetEntities(new SearchableGuess {Number = 1})).ToList();

        // Assert
        results.Should().NotBeEmpty();
        results.Should().OnlyContain(g => g.Number == 1);
    }

    [Test]
    public async Task GetEntities_IncludesWordAndLetters()
    {
        // Arrange
        var (conn, ctx) = TestDb.CreateContext();
        await using var _ = ctx;
        await using var __ = conn;

        TestDb.SeedBasicData(ctx);
        var sut = new GuessQueryService(ctx);

        // Act
        var guess = (await sut.GetEntities(new SearchableGuess {Number = 1})).First();

        // Assert
        guess.Word.Should().NotBeNull();
        guess.Word.Letters.Should().NotBeNull();
        guess.Word.Letters.Should().NotBeEmpty();
    }
}
