using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wordle.Model.Entity;
using Wordle.Persistence.Core;

namespace Wordle.Persistence.Configuration;

public class GuessConfiguration : EntityConfiguration<Guess>
{
    /// <inheritdoc />
    public override void Configure(EntityTypeBuilder<Guess> builder)
    {
        base.Configure(builder);

        builder.HasOne(x => (Game) x.Game).WithMany(x => (ICollection<Guess>) x.Guesses).HasForeignKey(x => x.GameId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => (Word) x.Word).WithMany().HasForeignKey(x => x.WordId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
