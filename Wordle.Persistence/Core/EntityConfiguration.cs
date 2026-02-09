using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wordle.Abstraction.Interfaces.Persistence;

namespace Wordle.Persistence.Core;

public abstract class EntityConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : class, IEntity
{
    /// <inheritdoc />
    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
        Type type = typeof(TEntity);
        var idName = $"{type.Name}Id";

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnName(idName);

        builder.Property(x => x.Version).IsRowVersion().HasConversion(new SqliteTimestampConverter())
            .HasColumnType("BLOB").HasDefaultValueSql("CURRENT_TIMESTAMP");
    }
}
