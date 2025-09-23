using Microsoft.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;
using Nayax.Dex.Repository.Configuration;

namespace Nayax.Dex.Application.IoC
{
    public static class DependencyInjectionSqlConnection
    {
        public static IServiceCollection AddSqlConnectionDependencyInjection(this IServiceCollection services)
        {
            services.AddScoped(provider =>
                CreateSqlConnection<LocalSqlConnection>("HNS8414", "NayaxDex"));

            return services;
        }

        private static T CreateSqlConnection<T>(string dataSource, string initialCatalog) where T : BaseSqlConnection, new()
        {
            try
            {
                var builder = new SqlConnectionStringBuilder
                {
                    DataSource = dataSource,
                    InitialCatalog = initialCatalog,
                    IntegratedSecurity = true,
                    TrustServerCertificate = true
                };

                var sqlConnection = new SqlConnection(builder.ToString());

                return new T
                {
                    SqlConnection = sqlConnection
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating SQL connection: {ex.Message}");
                throw;
            }
        }
    }
}
