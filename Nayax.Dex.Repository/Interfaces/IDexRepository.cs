using Nayax.Dex.CrossCutting.Models;

namespace Nayax.Dex.Repository.Interfaces
{
    public interface IDexRepository
    {
        Task<int> SaveDEXMeterAsync(DEXMeterModel dEXMeterModel);
        Task SaveDEXLaneMetersAsync(IEnumerable<DEXLaneMeterModel> laneMeters);
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
    }
}
