using Azure.Core;
using Azure.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;
using Nayax.Dex.Repository.Configuration;

namespace Nayax.Dex.Application.IoC
{
    public static class DependencyInjectionSqlConnection
    {
        private static readonly DefaultAzureCredential _credential
            = new(new DefaultAzureCredentialOptions());

        private static readonly Lock _lock = new();
        private static AccessToken _cachedToken;
        private static DateTimeOffset _tokenExpiry;

        public static IServiceCollection AddSqlConnectionDependencyInjection(this IServiceCollection services)
        {
            services.AddScoped(provider =>
                CreateSqlConnection<LocalSqlConnection>("LAPTOP-V2025", "Nayax"));

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
                    ConnectTimeout = 480,
                    Encrypt = true,
                    TrustServerCertificate = false
                };

                var sqlConnection = new SqlConnection(builder.ToString());
                UpdateAccessToken(sqlConnection);

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

        private static void UpdateAccessToken(SqlConnection sqlConnection)
        {
            lock (_lock)
            {
                var tokenRequestContext = new TokenRequestContext(new[] { "https://database.windows.net/.default" });

                if (_cachedToken.ExpiresOn <= DateTimeOffset.UtcNow)
                {
                    _cachedToken = _credential.GetToken(tokenRequestContext, default);
                    _tokenExpiry = _cachedToken.ExpiresOn;
                }

                sqlConnection.AccessToken = _cachedToken.Token;
            }
        }
    }
}
