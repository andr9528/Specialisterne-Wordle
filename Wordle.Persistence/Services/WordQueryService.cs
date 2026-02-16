using Microsoft.EntityFrameworkCore;
using Wordle.Model.Entity;
using Wordle.Model.Searchable;
using Wordle.Persistence.Core;

namespace Wordle.Persistence.Services;

public class WordQueryService : BaseEntityQueryService<WordleDatabaseContext, Word, SearchableWord>
{
    /// <inheritdoc />
    public WordQueryService(WordleDatabaseContext context) : base(context)
    {
    }

    /// <inheritdoc />
    protected override IQueryable<Word> GetBaseQuery()
    {
        return context.Words.AsQueryable().Include(x=>x.Letters);
    }

    /// <inheritdoc />
    protected override IQueryable<Word> AddQueryArguments(SearchableWord searchable, IQueryable<Word> query)
    {
        if (!string.IsNullOrWhiteSpace(searchable.Content))
        {
            query = query.Where(x => x.Content.ToLower() == searchable.Content.ToLower());
        }

        return query;
    }
}
