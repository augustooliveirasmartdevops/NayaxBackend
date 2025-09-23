using System.Globalization;
using System.Text.RegularExpressions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Nayax.Dex.Repository.Configuration;
using Nayax.Dex.Repository.Interfaces;

namespace Nayax.Dex.Repository.Entities
{
    public class DexRepository : BaseRepository, IDexRepository
    {
        private readonly NayaxDbContext _context;

        public DexRepository(NayaxDbContext context)
        {
            _context = context;
        }

        public async Task<bool> UploadDexFileAsync(string dexText)
        {
            await using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // -------- 1. Parse Header segments --------
                var machineId = Regex.Match(dexText, @"ID1\*([^\*]+)").Groups[1].Value;

                var serialMatch = Regex.Match(dexText, @"CB1\*([^*]+)");
                var machineSerial = serialMatch.Success ? serialMatch.Groups[1].Value.Split('*')[0] : string.Empty;

                var id5 = Regex.Match(dexText, @"ID5\*([0-9]{8})\*([0-9]{4})");
                DateTime dexDate = DateTime.UtcNow;
                if (id5.Success)
                {
                    var date = id5.Groups[1].Value;
                    var time = id5.Groups[2].Value;
                    dexDate = DateTime.ParseExact(date + time, "yyyyMMddHHmm", CultureInfo.InvariantCulture);
                }

                decimal valueOfPaidVends = 0m;
                var va1 = Regex.Match(dexText, @"VA1\*([0-9]+)");
                if (va1.Success)
                {
                    valueOfPaidVends = decimal.Parse(va1.Groups[1].Value) / 100m;
                }

                // -------- 2. Call SaveDEXMeter and get new ID --------
                var outParam = new SqlParameter("@NewId", System.Data.SqlDbType.Int)
                {
                    Direction = System.Data.ParameterDirection.Output
                };

                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC SaveDEXMeter @MachineID, @DEXDateTime, @MachineSerialNumber, @ValueOfPaidVends, @NewId OUTPUT",
                    new SqlParameter("@MachineID", machineId),
                    new SqlParameter("@DEXDateTime", dexDate),
                    new SqlParameter("@MachineSerialNumber", machineSerial),
                    new SqlParameter("@ValueOfPaidVends", valueOfPaidVends),
                    outParam);

                int dexMeterId = (int)outParam.Value;

                // -------- 3. Parse PA segments and call SaveDEXLaneMeter --------
                var pa1Matches = Regex.Matches(dexText, @"PA1\*([0-9]+)\*([0-9]+)");

                foreach (Match m in pa1Matches)
                {
                    string lane = m.Groups[1].Value;
                    decimal price = decimal.Parse(m.Groups[2].Value) / 100m;

                    // Number of vends & value from next PA2 line
                    // Looks for PA2 immediately after this PA1
                    var vendMatch = Regex.Match(
                        dexText,
                        $@"PA1\*{lane}\*[0-9]+\s*PA2\*(\d+)\*([0-9]+)",
                        RegexOptions.Singleline);

                    int numberOfVends = 0;
                    decimal valueOfPaidSales = 0m;
                    if (vendMatch.Success)
                    {
                        numberOfVends = int.Parse(vendMatch.Groups[1].Value);
                        valueOfPaidSales = decimal.Parse(vendMatch.Groups[2].Value) / 100m;
                    }

                    await _context.Database.ExecuteSqlRawAsync(
                        "EXEC SaveDEXLaneMeter @DEXMeterID, @ProductIdentifier, @Price, @NumberOfVends, @ValueOfPaidSales",
                        new SqlParameter("@DEXMeterID", dexMeterId),
                        new SqlParameter("@ProductIdentifier", Guid.NewGuid()),
                        new SqlParameter("@Price", price),
                        new SqlParameter("@NumberOfVends", numberOfVends),
                        new SqlParameter("@ValueOfPaidSales", valueOfPaidSales));
                }

                await transaction.CommitAsync();
                return true;
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}
