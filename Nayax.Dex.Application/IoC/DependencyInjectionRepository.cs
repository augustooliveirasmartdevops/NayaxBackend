using Microsoft.Extensions.DependencyInjection;
using Nayax.Dex.Repository.Entities;
using Nayax.Dex.Repository.Interfaces;

namespace Nayax.Dex.Application.IoC
{
    public static class DependencyInjectionRepository
    {
        public static IServiceCollection AddRepositoryDependencyInjection(this IServiceCollection services)
        {
            services.AddTransient<IUserRepository, UserRepository>();
            return services;
        }
    }
}
