using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Nayax.Dex.Domain.Entities.User;

namespace Nayax.Dex.Repository.Configuration
{
    public class NayaxDbContext : DbContext
    {
        private readonly SqlConnection _sqlConnection;
        public NayaxDbContext(DbContextOptions<NayaxDbContext> options, LocalSqlConnection localSqlConnection)
            : base(options)
        {
            _sqlConnection = localSqlConnection.SqlConnection;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_sqlConnection);
        }


        public required DbSet<UserDomain> User { get; set; }
    }
}
