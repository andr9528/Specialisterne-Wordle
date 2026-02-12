using Microsoft.EntityFrameworkCore;
using Wordle.Model.Entity;
using Wordle.Persistence.Configuration;
using Wordle.Persistence.Core;

namespace Wordle.Persistence;

public class WordleDatabaseContext : BaseDatabaseContext<WordleDatabaseContext>
{
    public const string CONNECTION_STRING = "Database.db";

    /// <inheritdoc />
    public WordleDatabaseContext(DbContextOptions<WordleDatabaseContext> options) : base(options)
    {
    }

    public virtual DbSet<Game> Games { get; set; }
    public virtual DbSet<Guess> Guesses { get; set; }
    public virtual DbSet<Word> Words { get; set; }
    public virtual DbSet<Letter> Letters { get; set; }

    /// <inheritdoc />
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new GameConfiguration());
        modelBuilder.ApplyConfiguration(new GuessConfiguration());
        modelBuilder.ApplyConfiguration(new WordConfiguration());
        modelBuilder.ApplyConfiguration(new LetterConfiguration());
    }
}
