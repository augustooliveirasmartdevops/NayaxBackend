using Nayax.Dex.Domain.Entities.User;

namespace Nayax.Dex.Repository.Interfaces
{
    public interface IUserRepository : IBaseRepository
    {
        Task<UserDomain> GetDefaultUserAsync();
    }
}
