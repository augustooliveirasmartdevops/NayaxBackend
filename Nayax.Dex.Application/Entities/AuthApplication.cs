using Microsoft.AspNetCore.Identity;
using Nayax.Dex.Application.Interfaces;
using Nayax.Dex.Domain.Entities.User;
using Nayax.Dex.Repository.Interfaces;

namespace Nayax.Dex.Application.Entities
{
    public class AuthApplication : IAuthApplication
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher<UserDomain> _hasher;

        public AuthApplication(IUserRepository userRepository, IPasswordHasher<UserDomain> hasher)
        {
            _userRepository = userRepository;
            _hasher = hasher;
        }

        public async Task<bool> ValidateCredentialsAsync(string userName, string password)
        {
            var userDomain = await _userRepository.GetDefaultUserAsync();
            if (userDomain is null)
            {
                return false;
            }

            if (!userDomain.UserName.Equals(userName, StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            var result = _hasher.VerifyHashedPassword(userDomain, userDomain.PasswordHash, password);
            return result == PasswordVerificationResult.Success;
        }
    }
}
