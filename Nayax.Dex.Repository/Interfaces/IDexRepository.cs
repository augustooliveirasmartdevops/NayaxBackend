namespace Nayax.Dex.Repository.Interfaces
{
    public interface IDexRepository
    {
        Task<bool> UploadDexFileAsync(string dexText);
    }
}
