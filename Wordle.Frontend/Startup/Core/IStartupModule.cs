namespace Wordle.Frontend.Startup.Core;

public interface IStartupModule
{
    /// <summary>
    ///     To be called during call to 'SetupServices', wherein different services are configured.
    /// </summary>
    /// <param name="services"></param>
    void ConfigureServices(IServiceCollection services);

    /// <summary>
    ///     To be called during call to 'SetupApplication', wherein the application is configured.
    /// </summary>
    /// <param name="app"></param>
    void ConfigureApplication(IApplicationBuilder app);
}
