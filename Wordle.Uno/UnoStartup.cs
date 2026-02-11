using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Wordle.Abstraction.Services;
using Wordle.Model.Entity;
using Wordle.Model.Searchable;
using Wordle.Model.Uno;
using Wordle.Persistence;
using Wordle.Persistence.Services;
using Wordle.Services;
using Wordle.Uno.Abstraction;
using Wordle.Uno.NavigationRegion;
using Wordle.Uno.Presentation.Region;
using Wordle.Uno.Startup;
using Wordle.Uno.Startup.Module;
using Path = System.IO.Path;

namespace Wordle.Uno;

public class UnoStartup : ModularStartup
{
    private const string CONNECTION_STRING = "Database.db";
    private const string LOG_FILE = "Assets/wordle.log";
    private const string APP_SETTINGS_FILE = "appsettings.json";

    public UnoStartup()
    {
        string basePath = AppContext.BaseDirectory;
        string dbPath = Path.Combine(basePath, CONNECTION_STRING);

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
            .UseLogging(ConfigureLogging, true).UseSerilog(true, true)
            .UseConfiguration(configure: ConfigureConfigurationSource).UseLocalization(ConfigureLocalization)
            .UseSerialization(ConfigureSerialization));

        base.ConfigureApplication(app);

        Host = app.Build();
    }

    private void ConfigureSerialization(HostBuilderContext host, IServiceCollection services)
    {
        services.AddSingleton(new JsonSerializerOptions { IncludeFields = true, });
    }

    private void ConfigureLogging(HostBuilderContext context, ILoggingBuilder logBuilder)
    {
        logBuilder.SetMinimumLevel(context.HostingEnvironment.IsDevelopment() ? LogLevel.Information : LogLevel.Warning)

            // Default filters for core Uno Platform namespaces
            .CoreLogLevel(LogLevel.Warning);

        // Uno Platform namespace filter groups
        // Uncomment individual methods to see more detailed logging
        //// Generic Xaml events
        //logBuilder.XamlLogLevel(LogLevel.Debug);
        //// Layout specific messages
        //logBuilder.XamlLayoutLogLevel(LogLevel.Debug);
        //// Storage messages
        //logBuilder.StorageLogLevel(LogLevel.Debug);
        //// Binding related messages
        //logBuilder.XamlBindingLogLevel(LogLevel.Debug);
        //// Binder memory references tracking
        //logBuilder.BinderMemoryReferenceLogLevel(LogLevel.Debug);
        //// DevServer and HotReload related
        //logBuilder.HotReloadCoreLogLevel(LogLevel.Information);
        //// Debug JS interop
        //logBuilder.WebAssemblyLogLevel(LogLevel.Debug);
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
