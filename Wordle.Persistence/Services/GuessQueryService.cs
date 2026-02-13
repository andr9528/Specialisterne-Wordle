using Microsoft.EntityFrameworkCore;
using Wordle.Model.Entity;
using Wordle.Model.Searchable;
using Wordle.Persistence.Core;

namespace Wordle.Persistence.Services;

public class GuessQueryService : BaseEntityQueryService<WordleDatabaseContext, Guess, SearchableGuess>
{
    /// <inheritdoc />
    public GuessQueryService(WordleDatabaseContext context) : base(context)
    {
    }

    /// <inheritdoc />
    protected override IQueryable<Guess> GetBaseQuery()
    {
        return context.Guesses.AsQueryable().Include(x=>x.Word).ThenInclude(x=>x.Letters);
    }

    /// <inheritdoc />
    protected override IQueryable<Guess> AddQueryArguments(SearchableGuess searchable, IQueryable<Guess> query)
    {
        if (searchable.Number != 0)
        {
            query = query.Where(x => x.Number == searchable.Number);
        }

        if (searchable.GameId != 0)
        {
            query = query.Where(x => x.GameId == searchable.GameId);
        }

        if (searchable.WordId != 0)
        {
            query = query.Where(x => x.WordId == searchable.WordId);
        }

        return query;
    }
}
