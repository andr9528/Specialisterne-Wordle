using System.Reflection;
using Wordle.Abstraction.Services;
using Wordle.Services;
using Wordle.Uno.Startup.Core;

namespace Wordle.Uno.Startup;

public class ModularStartup : IStartupModule
{
    protected ICollection<IStartupModule> _modules;

    protected ModularStartup()
    {
        _modules = new List<IStartupModule>();
    }

    public IServiceCollection Services { get; protected set; }
    public IServiceProvider ServiceProvider { get; protected set; }

    /// <inheritdoc />
    public virtual void ConfigureApplication(IApplicationBuilder app)
    {
    }

    /// <inheritdoc />
    public virtual void ConfigureServices(IServiceCollection services) {

    }

    protected void AddModule(IStartupModule module)
    {
        _modules.Add(module);
    }

    public void SetupServices(IServiceCollection? services = null)
    {
        Services = services ??= new ServiceCollection();

        ConfigureServices(services);
        foreach (IStartupModule module in _modules)
        {
            module.ConfigureServices(Services);
        }

        ServiceProvider = Services.BuildServiceProvider();
    }

    public IApplicationBuilder SetupApplication(IApplicationBuilder app)
    {
        ConfigureApplication(app);
        foreach (IStartupModule module in _modules)
        {
            module.ConfigureApplication(app);
        }

        return app;
    }
}
