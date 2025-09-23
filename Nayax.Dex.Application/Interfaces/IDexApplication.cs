namespace Nayax.Dex.Application.Interfaces
{
    public interface IDexApplication
    {
        Task UploadDexFileAsync(string dexText);
    }
}
