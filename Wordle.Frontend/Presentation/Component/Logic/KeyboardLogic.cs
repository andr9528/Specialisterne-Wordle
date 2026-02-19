using Wordle.Abstraction.Services;
using Wordle.Frontend.Abstraction;
using Wordle.Frontend.Presentation.Component.ViewModel;

namespace Wordle.Frontend.Presentation.Component.Logic;

public sealed class KeyboardLogic
{
    private readonly KeyboardViewModel viewModel;
    private readonly IGameService gameService;
    private readonly IUiDispatcher dispatcher;
    private readonly ILogger<KeyboardLogic> logger;
    private readonly Brush defaultBorderBrush = new SolidColorBrush(Colors.Gray);
    private readonly Brush invalidBorderBrush = new SolidColorBrush(Colors.Red);
    private bool processingSubmit = false;

    public KeyboardLogic(KeyboardViewModel viewModel, IGameService gameService, IUiDispatcher dispatcher, ILogger<KeyboardLogic> logger)
    {
        this.viewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));
        this.gameService = gameService;
        this.dispatcher = dispatcher;
        this.logger = logger;
    }

    public async Task SubmitOnClick(object sender, RoutedEventArgs e)
    {
        try
        {
            if (processingSubmit)
            {
                return;
            }
            processingSubmit = true;
            var isValidGuess = await gameService.ProcessGuess(viewModel.CurrentGuess);

            if (!isValidGuess)
            {
                _ = Task.Run(() =>
                {
                    logger.LogDebug("Guess was invalid, so changing border colour to red briefly.");
                    dispatcher.Enqueue(() => viewModel.InputBorderBrush = invalidBorderBrush);
                    Thread.Sleep(TimeSpan.FromSeconds(4));
                    logger.LogDebug("Changing border colour back to gray now.");
                    dispatcher.Enqueue(() => viewModel.InputBorderBrush = defaultBorderBrush);
                });

                return;
            }

            dispatcher.Enqueue(() => viewModel.CurrentGuess = string.Empty);
        }
        catch (Exception exe)
        {
            logger.LogError(exe, $"Exception thrown inside {nameof(SubmitOnClick)}");
            throw;
        }
        finally
        {
            processingSubmit = false;
        }
    }
}
