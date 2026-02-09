using Wordle.Abstraction.Interfaces.Persistence;
using Wordle.Uno.Startup.Core;

namespace Wordle.Uno.Startup.Module;

public class EntityQueryServiceStartupModule<TQuery, TEntity, TSearchable> : IStartupModule
    where TQuery : class, IEntityQueryService<TEntity, TSearchable>
    where TEntity : class, IEntity
    where TSearchable : class, ISearchable
{
    /// <inheritdoc />
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddScoped<IEntityQueryService<TEntity, TSearchable>, TQuery>();
    }

    /// <inheritdoc />
    public void ConfigureApplication(IApplicationBuilder app)
    {
    }
}
