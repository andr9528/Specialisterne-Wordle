using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Wordle.Persistence;

public class WordleDbDesignTimeContextFactory: IDesignTimeDbContextFactory<WordleDatabaseContext>
{
    public WordleDatabaseContext CreateDbContext(string[] args)
    {
        var basePath = Directory.GetCurrentDirectory();
        var dbPath = Path.Combine(basePath, WordleDatabaseContext.CONNECTION_STRING);

        var options = new DbContextOptionsBuilder<WordleDatabaseContext>().UseSqlite($"Data Source={dbPath}").Options;

        return new WordleDatabaseContext(options);
    }
}
