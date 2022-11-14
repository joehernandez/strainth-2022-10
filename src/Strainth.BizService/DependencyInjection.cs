using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Strainth.BizService.Repositories.Setup;
using Strainth.BizService.Repositories.UoW;

namespace Strainth.BizService;
public static class DependencyInjection
{
    public static IServiceCollection AddBizService(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<StrainthContext>(options =>
            options.UseSqlServer(config.GetConnectionString("StrainthConnection")));
        services.AddScoped<IExercisesRepository, ExercisesRepository>();
        services.AddScoped<ICategoriesRepository, CategoriesRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}
