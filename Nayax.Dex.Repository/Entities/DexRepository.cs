using Microsoft.EntityFrameworkCore;
using Nayax.Dex.Domain.Entities.DataExchange;
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

        public async Task<DEXMeterDomain> UploadDexFileAsync()
        {
            try
            {

                var response1 = await _context.DEXMeter
                    .FirstOrDefaultAsync();

                var response = await _context.DEXMeter
                    .Where(x => x.MachineId == "dsds")
                    .OrderBy(o => o.DEXDateTime)
                    .SingleAsync();

                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
