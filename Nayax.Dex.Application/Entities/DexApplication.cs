using Nayax.Dex.Application.Interfaces;
using Nayax.Dex.Repository.Interfaces;

namespace Nayax.Dex.Application.Entities
{
    public class DexApplication : IDexApplication
    {
        private readonly IDexRepository _dexRepository;

        public DexApplication(IDexRepository dexRepository)
        {
            _dexRepository = dexRepository;
        }

        public async Task UploadDexFileAsync(string dexText)
        {
            await _dexRepository.UploadDexFileAsync(dexText);
        }
    }
}
