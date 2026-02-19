using Microsoft.Extensions.Logging;
using Wordle.Abstraction.Interfaces.Model.Entity;
using Wordle.Abstraction.Services;
using Wordle.Model.Entity;

namespace Wordle.Services;

public class FileWordService : BaseWordService<FileWordService>
{
    private const string EMBEDDED_WORD_LIST_NAME = "Wordle.Services.Assets.wordle_ord.txt";

    public FileWordService(ILogger<FileWordService> logger) : base(logger)
    {
    }

    protected override async Task<IList<string>> LoadWords()
    {
        var asm = typeof(FileWordService).Assembly;

        await using var stream = asm.GetManifestResourceStream(EMBEDDED_WORD_LIST_NAME);
        if (stream is null)
        {
            var available = string.Join(Environment.NewLine, asm.GetManifestResourceNames());
            throw new FileNotFoundException(
                $"Embedded resource '{EMBEDDED_WORD_LIST_NAME}' not found. Available resources:\n{available}");
        }

        logger.LogInformation("Loading words from embedded file");

        var words = await ReadRawWords(stream);
        var cleanedWords = words.Where(line => !string.IsNullOrWhiteSpace(line)).Skip(1).Select(line => line.Trim())
            .ToList();

        logger.LogInformation($"Loaded {cleanedWords.Count} words into cache.");

        return cleanedWords;
    }

    private async Task<List<string>> ReadRawWords(Stream stream)
    {
        using var reader = new StreamReader(stream);
        var words = new List<string>();
        while (!reader.EndOfStream)
        {
            var line = await reader.ReadLineAsync().ConfigureAwait(false);
            if (string.IsNullOrWhiteSpace(line))
                continue;

            var word = line.Trim();
            words.Add(word);
        }

        return words;
    }
}

