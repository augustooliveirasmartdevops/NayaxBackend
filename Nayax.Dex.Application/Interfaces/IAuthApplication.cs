namespace Nayax.Dex.Application.Interfaces
{
    public interface IAuthApplication
    {
        Task<bool> ValidateCredentialsAsync(string userName, string password);
    }
}
