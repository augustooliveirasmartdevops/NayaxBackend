using Microsoft.AspNetCore.Identity;

namespace Nayax.Dex.Domain.Entities.User
{
    public class UserDomain
    {
        public string UserName { get; private set; }
        public string PasswordHash { get; private set; }

        public UserDomain()
        {
            UserName = "vendsys";
            PasswordHash = new PasswordHasher<UserDomain>().HashPassword(this, "NFsZGmHAGWJSZ#RuvdiV)");
        }

    }
}
