namespace Nayax.Dex.Application.Interfaces
{
    public interface IDexApplication
    {
        Task<bool> UploadDexFileAsync();
    }
}
