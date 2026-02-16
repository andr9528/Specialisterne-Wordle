using Wordle.Model.Entity;
using Wordle.Model.Searchable;
using Wordle.Persistence.Services;

namespace Wordle.Tests.Services.Persistence;

[TestFixture]
public sealed class BaseEntityQueryServiceTests
{
    [Test]
    public async Task AddEntity_WhenSaveImmediatelyTrue_Persists()
    {
        // Arrange
        var (conn, ctx) = TestDb.CreateContext();
        await using var _ = ctx;
        await using var __ = conn;

        var sut = new LetterQueryService(ctx);
        var word = new Word() {Content = "x"};
        var entity = new Letter { Content = 'x', Position = 7, Word = word};

        // Act
        await sut.AddEntity(entity, saveImmediately: true);

        // Assert
        ctx.Letters.Any(l => l.Content == 'x' && l.Position == 7).Should().BeTrue();
    }

    [Test]
    public async Task AddEntities_WhenSaveImmediatelyTrue_PersistsAll()
    {
        // Arrange
        var (conn, ctx) = TestDb.CreateContext();
        await using var _ = ctx;
        await using var __ = conn;

        var sut = new LetterQueryService(ctx);
        var word = new Word() {Content = "xy"};

        var entities = new[]
        {
            new Letter { Content = 'x', Position = 1, Word = word},
            new Letter { Content = 'y', Position = 2, Word = word},
        };

        // Act
        await sut.AddEntities(entities, saveImmediately: true);

        // Assert
        ctx.Letters.Count(l => l.Content == 'x' || l.Content == 'y').Should().Be(2);
    }

    [Test]
    public async Task GetEntity_WhenMatchExists_ReturnsFirst()
    {
        // Arrange
        var (conn, ctx) = TestDb.CreateContext();
        await using var _ = ctx;
        await using var __ = conn;

        TestDb.SeedBasicData(ctx);
        var sut = new LetterQueryService(ctx);

        var existing = ctx.Letters.First();
        var searchable = new SearchableLetter { Id = existing.Id };

        // Act
        var result = await sut.GetEntity(searchable);

        // Assert
        result.Id.Should().Be(existing.Id);
    }

    [Test]
    public async Task GetEntity_WhenNoMatch_ThrowsInvalidOperationException()
    {
        // Arrange
        var (conn, ctx) = TestDb.CreateContext();
        await using var _ = ctx;
        await using var __ = conn;

        var sut = new LetterQueryService(ctx);
        var searchable = new SearchableLetter { Id = 999999 };

        // Act
        Func<Task> act = async () => await sut.GetEntity(searchable);

        // Assert
        await act.Should().ThrowAsync<InvalidOperationException>()
            .WithMessage("*no elements*");
    }

    [Test]
    public async Task GetEntities_WhenMatchExists_ReturnsMatches()
    {
        // Arrange
        var (conn, ctx) = TestDb.CreateContext();
        await using var _ = ctx;
        await using var __ = conn;

        TestDb.SeedBasicData(ctx);
        var sut = new LetterQueryService(ctx);

        // Act
        var results = (await sut.GetEntities(new SearchableLetter { Position = 1 })).ToList();

        // Assert
        results.Should().NotBeEmpty();
        results.Should().OnlyContain(x => x.Position == 1);
    }

    [Test]
    public async Task UpdateEntity_WhenSaveImmediatelyTrue_PersistsChanges()
    {
        // Arrange
        var (conn, ctx) = TestDb.CreateContext();
        await using var _ = ctx;
        await using var __ = conn;

        TestDb.SeedBasicData(ctx);
        var sut = new LetterQueryService(ctx);

        var letter = ctx.Letters.First();
        letter.Position = 42;

        // Act
        await sut.UpdateEntity(letter, saveImmediately: true);

        // Assert
        ctx.Letters.Single(x => x.Id == letter.Id).Position.Should().Be(42);
    }

    [Test]
    public async Task UpdateEntities_WhenSaveImmediatelyTrue_PersistsAllChanges()
    {
        // Arrange
        var (conn, ctx) = TestDb.CreateContext();
        await using var _ = ctx;
        await using var __ = conn;

        TestDb.SeedBasicData(ctx);
        var sut = new LetterQueryService(ctx);

        var letters = ctx.Letters.Take(2).ToList();
        letters[0].Position = 10;
        letters[1].Position = 11;

        // Act
        await sut.UpdateEntities(letters, saveImmediately: true);

        // Assert
        ctx.Letters.Single(x => x.Id == letters[0].Id).Position.Should().Be(10);
        ctx.Letters.Single(x => x.Id == letters[1].Id).Position.Should().Be(11);
    }

    [Test]
    public async Task DeleteEntity_WhenMatchExists_RemovesIt()
    {
        // Arrange
        var (conn, ctx) = TestDb.CreateContext();
        await using var _ = ctx;
        await using var __ = conn;

        TestDb.SeedBasicData(ctx);
        var sut = new LetterQueryService(ctx);

        var letter = ctx.Letters.First();

        // Act
        await sut.DeleteEntity(new SearchableLetter { Id = letter.Id }, saveImmediately: true);

        // Assert
        ctx.Letters.Any(x => x.Id == letter.Id).Should().BeFalse();
    }

    [Test]
    public async Task DeleteEntityById_WhenExists_RemovesIt()
    {
        // Arrange
        var (conn, ctx) = TestDb.CreateContext();
        await using var _ = ctx;
        await using var __ = conn;

        TestDb.SeedBasicData(ctx);
        var sut = new LetterQueryService(ctx);

        var letter = ctx.Letters.First();

        // Act
        await sut.DeleteEntityById(letter.Id, saveImmediately: true);

        // Assert
        ctx.Letters.Any(x => x.Id == letter.Id).Should().BeFalse();
    }
}
