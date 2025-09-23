using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Nayax.Dex.Domain.Entities.DataExchange;

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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DEXMeterDomain>()
                .HasKey(b => b.Id);

            modelBuilder.Entity<DEXLaneMeterDomain>()
               .HasKey(b => b.Id);
        }


        public required DbSet<DEXMeterDomain> DEXMeter { get; set; }
        public required DbSet<DEXLaneMeterDomain> DEXLaneMeter { get; set; }
    }
}
