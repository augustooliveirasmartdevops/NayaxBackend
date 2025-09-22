using Nayax.Dex.Domain.Entities.User;
using Nayax.Dex.Repository.Interfaces;

namespace Nayax.Dex.Repository.Entities
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public async Task<UserDomain> GetDefaultUserAsync()
        {
            var response = await Task.FromResult(new UserDomain());
            return response;
        }
    }
}
