using Nayax.Dex.Domain.Entities.DataExchange;

namespace Nayax.Dex.Repository.Interfaces
{
    public interface IDexRepository
    {
        Task<DEXMeterDomain> UploadDexFileAsync();
    }
}
