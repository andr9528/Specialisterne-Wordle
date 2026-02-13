using Microsoft.EntityFrameworkCore;
using Wordle.Model.Entity;
using Wordle.Model.Searchable;
using Wordle.Persistence.Core;

namespace Wordle.Persistence.Services;

public class GameQueryService : BaseEntityQueryService<WordleDatabaseContext, Game, SearchableGame>
{
    /// <inheritdoc />
    public GameQueryService(WordleDatabaseContext context) : base(context)
    {
    }

    /// <inheritdoc />
    protected override IQueryable<Game> GetBaseQuery()
    {
        return context.Games.AsQueryable()
            .Include(x => x.Word).ThenInclude(x => x.Letters)
            .Include(x => x.Guesses).ThenInclude(x=>x.Word).ThenInclude(x=>x.Letters);
    }

    /// <inheritdoc />
    protected override IQueryable<Game> AddQueryArguments(SearchableGame searchable, IQueryable<Game> query)
    {
        if (searchable.WordId != 0)
        {
            query = query.Where(x => x.WordId == searchable.WordId);
        }

        if (searchable.AttemptsLeft != 0)
        {
            query = query.Where(x => x.AttemptsLeft == searchable.AttemptsLeft);
        }

        if (!Equals(searchable.GameState, default(Abstraction.Enums.GameState)))
        {
            query = query.Where(x => x.GameState == searchable.GameState);
        }

        return query;
    }
}
