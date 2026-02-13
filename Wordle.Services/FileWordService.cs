using Microsoft.Extensions.Logging;
using Wordle.Abstraction.Interfaces.Model.Entity;
using Wordle.Abstraction.Services;
using Wordle.Model.Entity;

namespace Wordle.Services;

public class FileWordService : BaseWordService<FileWordService>
{
    private readonly string filePath;

    public FileWordService(ILogger<FileWordService> logger, string filePath = "Assets/wordle_ord.txt") : base(logger)
    {
        this.filePath = filePath;
    }

    protected override async Task<IList<string>> LoadWords()
    {
        if (!File.Exists(filePath))
            throw new FileNotFoundException($"Could not find words file at path: {filePath}");

        logger.LogInformation("Loading words from file at '{FilePath}'.", filePath);
        var lines = await File.ReadAllLinesAsync(filePath);

        return lines.Where(line => !string.IsNullOrWhiteSpace(line)).Select(line => line.Trim()).ToList();
    }
}

