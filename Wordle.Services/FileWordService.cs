using Wordle.Abstraction.Interfaces.Model.Entity;
using Wordle.Abstraction.Services;
using Wordle.Extensions;
using Wordle.Model.Entity;

namespace Wordle.Services;

public class FileWordService : BaseWordService
{
    private readonly string filePath;

    public FileWordService(string filePath = "Assets/wordle_ord.txt")
    {
        this.filePath = filePath;
    }

    protected override async Task<IList<string>> LoadWords()
    {
        if (!File.Exists(filePath))
            throw new FileNotFoundException($"Could not find words file at path: {filePath}");

        var lines = await File.ReadAllLinesAsync(filePath);

        return lines.Where(line => !string.IsNullOrWhiteSpace(line)).Select(line => line.Trim()).ToList();
    }
}

