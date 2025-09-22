using Microsoft.Extensions.DependencyInjection;

namespace Nayax.Dex.Application.IoC
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDependenciesInjection(this IServiceCollection services)
        {
            services.AddApplicationDependencyInjection();
            services.AddRepositoryDependencyInjection();
            services.AddSqlConnectionDependencyInjection();
            return services;
        }
    }
}
