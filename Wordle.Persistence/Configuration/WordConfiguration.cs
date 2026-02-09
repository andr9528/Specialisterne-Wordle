using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wordle.Model.Entity;
using Wordle.Persistence.Core;

namespace Wordle.Persistence.Configuration;

public class WordConfiguration : EntityConfiguration<Word>
{
    /// <inheritdoc />
    public override void Configure(EntityTypeBuilder<Word> builder)
    {
        base.Configure(builder);

        builder.HasMany(x => (ICollection<Letter>) x.Letters).WithOne(x => (Word) x.Word).HasForeignKey(x => x.WordId)
            .OnDelete(DeleteBehavior.Cascade);
        ;
    }
}
