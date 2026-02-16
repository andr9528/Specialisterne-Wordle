using Wordle.Abstraction.Enums;
using Wordle.Model.Searchable;
using Wordle.Persistence.Services;

namespace Wordle.Tests.Services.Persistence;

[TestFixture]
public sealed class LetterQueryServiceTests
{
    [Test]
    public async Task GetEntities_WhenContentMatches_CaseInsensitive_ReturnsMatches()
    {
        // Arrange
        var (conn, ctx) = TestDb.CreateContext();
        await using var _ = ctx;
        await using var __ = conn;

        TestDb.SeedBasicData(ctx);
        var sut = new LetterQueryService(ctx);

        // Act
        var results = (await sut.GetEntities(new SearchableLetter {Content = 'a'})).ToList();

        // Assert
        results.Should().NotBeEmpty();
        results.Should().OnlyContain(l => char.ToUpperInvariant(l.Content) == 'A');
    }

    [Test]
    public async Task GetEntities_WhenPositionMatches_ReturnsMatches()
    {
        // Arrange
        var (conn, ctx) = TestDb.CreateContext();
        await using var _ = ctx;
        await using var __ = conn;

        TestDb.SeedBasicData(ctx);
        var sut = new LetterQueryService(ctx);

        // Act
        var results = (await sut.GetEntities(new SearchableLetter {Position = 1})).ToList();

        // Assert
        results.Should().NotBeEmpty();
        results.Should().OnlyContain(l => l.Position == 1);
    }

    [Test]
    public async Task GetEntities_WhenCharacterStateMatches_ReturnsMatches()
    {
        // Arrange
        var (conn, ctx) = TestDb.CreateContext();
        await using var _ = ctx;
        await using var __ = conn;

        TestDb.SeedBasicData(ctx);
        var sut = new LetterQueryService(ctx);

        // Act
        var results = (await sut.GetEntities(new SearchableLetter {CharacterState = CharacterState.CORRECT})).ToList();

        // Assert
        results.Should().NotBeEmpty();
        results.Should().OnlyContain(l => l.CharacterState == CharacterState.CORRECT);
    }
}
