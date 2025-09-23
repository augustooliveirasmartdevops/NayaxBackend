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

        public async Task<bool> UploadDexFileAsync()
        {
            var dexDomain = await _dexRepository.UploadDexFileAsync();
            if (dexDomain is null)
            {
                return false;
            }

            return true;
        }
    }
}
