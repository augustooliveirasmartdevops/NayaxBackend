using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Nayax.Dex.Application.Entities;
using Nayax.Dex.Application.Interfaces;
using Nayax.Dex.Domain.Entities.User;

namespace Nayax.Dex.Application.IoC
{
    public static class DependencyInjectionApplication
    {
        public static IServiceCollection AddApplicationDependencyInjection(this IServiceCollection services)
        {
            services.AddTransient<IAuthApplication, AuthApplication>();
            services.AddTransient<IDexApplication, DexApplication>();
            services.AddTransient<IPasswordHasher<UserDomain>, PasswordHasher<UserDomain>>();
            return services;
        }
    }
}
