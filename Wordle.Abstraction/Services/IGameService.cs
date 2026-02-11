namespace Wordle.Abstraction.Services;

public interface IGameService
{
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public Task StartNewGame();
    /// <summary>
    /// 
    /// </summary>
    /// <param name="guessedWord"></param>
    /// <returns>True if it is a valid guess, false otherwise</returns>
    public Task<bool> ProcessGuess(string guessedWord);
}
