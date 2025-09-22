using Azure.Core;
using Azure.Identity;
using Microsoft.Data.SqlClient;

namespace Nayax.Dex.Repository.Configuration
{
    public abstract class BaseSqlConnection
    {
        public SqlConnection SqlConnection { get; set; } = null!;

        public void RefreshToken(DefaultAzureCredential credential, TokenRequestContext tokenRequestContext)
        {
            try
            {
                var authResult = credential.GetToken(tokenRequestContext, default);
                SqlConnection.AccessToken = authResult.Token;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error refreshing token: {ex.Message}");
                throw;
            }
        }
    }
}
