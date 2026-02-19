using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.UI.Dispatching;
using Wordle.Abstraction.Services;
using Wordle.Frontend.Abstraction;
using Wordle.Frontend.NavigationRegion;
using Wordle.Frontend.Presentation.Core;
using Wordle.Frontend.Presentation.Factory;
using Wordle.Frontend.Startup;
using Wordle.Frontend.Startup.Module;
using Wordle.Model.Entity;
using Wordle.Model.Searchable;
using Wordle.Model.Uno;
using Wordle.Persistence;
using Wordle.Persistence.Services;
using Wordle.Services;
using Path = System.IO.Path;

namespace Wordle.Frontend;

public class UnoStartup : ModularStartup
{
    private const string APP_SETTINGS_FILE = "appsettings.json";

    public UnoStartup()
    {
        string basePath = AppContext.BaseDirectory;
        string dbPath = Path.Combine(basePath, WordleDatabaseContext.CONNECTION_STRING);

        AddModule(new LoggingStartupModule());
        AddModule(new DatabaseContextStartupModule<WordleDatabaseContext>(options =>
            options.UseSqlite($"Data Source={dbPath}")));

        AddModule(new EntityQueryServiceStartupModule<GameQueryService, Game, SearchableGame>());
        AddModule(new EntityQueryServiceStartupModule<GuessQueryService, Guess, SearchableGuess>());
        AddModule(new EntityQueryServiceStartupModule<WordQueryService, Word, SearchableWord>());
        AddModule(new EntityQueryServiceStartupModule<LetterQueryService, Letter, SearchableLetter>());
    }

    protected IHost? Host { get; private set; }

    /// <inheritdoc />
    public override void ConfigureServices(IServiceCollection services)
    {
        base.ConfigureServices(services);

        services.AddSingleton<IGameService, GameService>();
        services.AddScoped<IGuessService, GuessService>();
        services.AddScoped<IWordService, FileWordService>();

        var uiDispatcherQueue = DispatcherQueue.GetForCurrentThread();
        services.AddSingleton<IUiDispatcher>(new UiDispatcher(uiDispatcherQueue));
        services.AddSingleton<IViewModelFactory, ViewModelFactory>();

        services.AddSingleton<IPageRegion, GamePageRegionDefinition>();
        services.AddSingleton<IPageRegion, HistoryPageRegionDefinition>();
    }

    /// <inheritdoc />
    public override void ConfigureApplication(IApplicationBuilder app)
    {
        app.Configure(host => host
#if DEBUG
            // Switch to Development environment when running in DEBUG
            .UseEnvironment(Environments.Development)
#endif
            .UseConfiguration(configure: ConfigureConfigurationSource).UseLocalization(ConfigureLocalization)
            .UseSerialization(ConfigureSerialization));

        base.ConfigureApplication(app);

        Host = app.Build();
    }

    private void ConfigureSerialization(HostBuilderContext host, IServiceCollection services)
    {
        services.AddSingleton(new JsonSerializerOptions { IncludeFields = true, });
    }

    private IHostBuilder ConfigureConfigurationSource(IConfigBuilder configBuilder)
    {
        return configBuilder.EmbeddedSource<App>().Section<AppConfig>();
    }

    private void ConfigureLocalization(HostBuilderContext context, IServiceCollection services)
    {
        // Enables localization (see appsettings.json for supported languages)
    }
}
