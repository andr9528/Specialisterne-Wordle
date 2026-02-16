using Wordle.Abstraction.Enums;
using Wordle.Model.Searchable;
using Wordle.Persistence.Services;

namespace Wordle.Tests.Services.Persistence;

[TestFixture]
public sealed class GameQueryServiceTests
{
    [Test]
    public async Task GetEntities_WhenFilteringByGameState_ReturnsMatches()
    {
        // Arrange
        var (conn, ctx) = TestDb.CreateContext();
        await using var _ = ctx;
        await using var __ = conn;

        TestDb.SeedBasicData(ctx);
        var sut = new GameQueryService(ctx);

        // Act
        var results = (await sut.GetEntities(new SearchableGame {GameState = GameState.ONGOING})).ToList();

        // Assert
        results.Should().NotBeEmpty();
        results.Should().OnlyContain(g => g.GameState == GameState.ONGOING);
    }

    [Test]
    public async Task GetEntities_IncludesSolutionWordLetters_And_GuessesWordLetters()
    {
        // Arrange
        var (conn, ctx) = TestDb.CreateContext();
        await using var _ = ctx;
        await using var __ = conn;

        TestDb.SeedBasicData(ctx);
        var sut = new GameQueryService(ctx);

        // Act
        var game = (await sut.GetEntities(new SearchableGame {GameState = GameState.ONGOING})).First();

        // Assert
        game.Word.Should().NotBeNull();
        game.Word.Letters.Should().NotBeNull();
        game.Word.Letters.Should().NotBeEmpty();

        game.Guesses.Should().NotBeNull();
        game.Guesses.Should().NotBeEmpty();

        var firstGuess = game.Guesses.First();
        firstGuess.Word.Should().NotBeNull();
        firstGuess.Word.Letters.Should().NotBeNull();
        firstGuess.Word.Letters.Should().NotBeEmpty();
    }
}
