using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Nayax.Dex.CrossCutting.Models;
using Nayax.Dex.Repository.Configuration;
using Nayax.Dex.Repository.Interfaces;

namespace Nayax.Dex.Repository.Entities
{
    public class DexRepository : BaseRepository, IDexRepository
    {
        private readonly NayaxDbContext _context;
        private IDbContextTransaction _transaction;

        public DexRepository(NayaxDbContext context)
        {
            _context = context;
        }

        public async Task BeginTransactionAsync()
        {
            _transaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            if (_transaction != null)
            {
                await _transaction.CommitAsync();
                await _transaction.DisposeAsync();
            }
        }

        public async Task RollbackTransactionAsync()
        {
            if (_transaction != null)
            {
                await _transaction.RollbackAsync();
                await _transaction.DisposeAsync();
            }
        }

        public async Task<int> SaveDEXMeterAsync(DEXMeterModel dEXMeterModel)
        {
            var outParam = new SqlParameter("@NewId", System.Data.SqlDbType.Int)
            {
                Direction = System.Data.ParameterDirection.Output
            };

            await _context.Database.ExecuteSqlRawAsync(
                "EXEC SaveDEXMeter @MachineID, @DEXDateTime, @MachineSerialNumber, @ValueOfPaidVends, @NewId OUTPUT",
                new SqlParameter("@MachineID", dEXMeterModel.MachineId),
                new SqlParameter("@DEXDateTime", dEXMeterModel.DEXDateTime),
                new SqlParameter("@MachineSerialNumber", dEXMeterModel.MachineSerialNumber),
                new SqlParameter("@ValueOfPaidVends", dEXMeterModel.ValueOfPaidVends),
                outParam);

            return (int)outParam.Value;
        }

        public async Task SaveDEXLaneMetersAsync(IEnumerable<DEXLaneMeterModel> laneMeters)
        {
            foreach (var laneMeter in laneMeters)
            {
                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC SaveDEXLaneMeter @DEXMeterID, @ProductIdentifier, @Price, @NumberOfVends, @ValueOfPaidSales",
                    new SqlParameter("@DEXMeterID", laneMeter.DEXMeterId),
                    new SqlParameter("@ProductIdentifier", laneMeter.ProductIdentifier),
                    new SqlParameter("@Price", laneMeter.Price),
                    new SqlParameter("@NumberOfVends", laneMeter.NumberOfVends),
                    new SqlParameter("@ValueOfPaidSales", laneMeter.ValueOfPaidSales));
            }
        }

    }
}
