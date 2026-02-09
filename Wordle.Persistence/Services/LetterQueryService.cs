using Wordle.Model.Entity;
using Wordle.Model.Searchable;
using Wordle.Persistence.Core;

namespace Wordle.Persistence.Services;

public class LetterQueryService : BaseEntityQueryService<WordleDatabaseContext, Letter, SearchableLetter>
{
    /// <inheritdoc />
    public LetterQueryService(WordleDatabaseContext context) : base(context)
    {
    }

    /// <inheritdoc />
    protected override IQueryable<Letter> GetBaseQuery()
    {
        return context.Letters.AsQueryable();
    }

    /// <inheritdoc />
    protected override IQueryable<Letter> AddQueryArguments(SearchableLetter searchable, IQueryable<Letter> query)
    {
        if (searchable.Content != '\0')
        {
            query = query.Where(x => char.ToUpperInvariant(x.Content) == char.ToUpperInvariant(searchable.Content));
        }

        if (searchable.Position != 0)
        {
            query = query.Where(x => x.Position == searchable.Position);
        }

        if (searchable.WordId != 0)
        {
            query = query.Where(x => x.WordId == searchable.WordId);
        }

        return query;
    }
}
