using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wordle.Model.Entity;
using Wordle.Persistence.Core;

namespace Wordle.Persistence.Configuration;

public class LetterConfiguration : EntityConfiguration<Letter>
{
    /// <inheritdoc />
    public override void Configure(EntityTypeBuilder<Letter> builder)
    {
        base.Configure(builder);

        builder.HasOne(x => (Word) x.Word).WithMany(x => (ICollection<Letter>) x.Letters).HasForeignKey(x => x.WordId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(x => x.CharacterState).HasConversion<string>();

    }
}
