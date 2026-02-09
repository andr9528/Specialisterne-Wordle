using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wordle.Model.Entity;
using Wordle.Persistence.Core;

namespace Wordle.Persistence.Configuration;

public class GameConfiguration : EntityConfiguration<Game>
{
    /// <inheritdoc />
    public override void Configure(EntityTypeBuilder<Game> builder)
    {
        base.Configure(builder);

        builder.HasOne(x => (Word) x.Word).WithOne().HasForeignKey<Game>(x => x.WordId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
