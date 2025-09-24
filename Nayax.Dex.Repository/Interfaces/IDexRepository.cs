using Nayax.Dex.CrossCutting.Models;

namespace Nayax.Dex.Repository.Interfaces
{
    public interface IDexRepository
    {
        Task<bool> UploadDexFileAsync(string dexText);
        Task<int> SaveDEXMeterAsync(DEXMeterModel dEXMeterModel);
        Task SaveDEXLaneMetersAsync(IEnumerable<DEXLaneMeterModel> laneMeters);
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
    }
}
