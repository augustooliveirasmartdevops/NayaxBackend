using System.Globalization;
using System.Text.RegularExpressions;
using Nayax.Dex.Application.Interfaces;
using Nayax.Dex.CrossCutting.Models;
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
            await _dexRepository.BeginTransactionAsync();

            try
            {
                var id1Match = Regex.Match(dexText, @"ID1\*([^\r\n]+)");
                string machineId = string.Empty;
                if (id1Match.Success)
                {
                    var parts = id1Match.Groups[1].Value.Split('*');
                    if (parts.Length >= 6)
                    {
                        machineId = parts[5];
                    }
                }

                var serialMatch = Regex.Match(dexText, @"CB1\*([^*]+)");
                var machineSerial = serialMatch.Success ? serialMatch.Groups[1].Value.Split('*')[0] : string.Empty;

                var id5 = Regex.Match(dexText, @"ID5\*([0-9]{8})\*([0-9]{4})");
                var dexDate = DateTime.UtcNow;
                if (id5.Success)
                {
                    var date = id5.Groups[1].Value;
                    var time = id5.Groups[2].Value;
                    dexDate = DateTime.ParseExact(date + time, "yyyyMMddHHmm", CultureInfo.InvariantCulture);
                }

                decimal valueOfPaidVends = 0m;
                var va1 = Regex.Match(dexText, @"VA1\*([0-9]+)");
                if (va1.Success)
                {
                    valueOfPaidVends = decimal.Parse(va1.Groups[1].Value) / 100m;
                }

                var dEXMeterModel = new DEXMeterModel(machineId, dexDate, machineSerial, valueOfPaidVends);

                int dexMeterId = await _dexRepository.SaveDEXMeterAsync(dEXMeterModel);

                var pa1Matches = Regex.Matches(dexText, @"PA1\*([0-9]+)\*([0-9]+)");
                var dexLaneMeters = new List<DEXLaneMeterModel>();

                foreach (Match m in pa1Matches)
                {
                    string lane = m.Groups[1].Value;
                    decimal price = decimal.Parse(m.Groups[2].Value) / 100m;

                    int numberOfVends = 0;
                    decimal valueOfPaidSales = 0m;
                    var vendMatch = Regex.Match(
                        dexText,
                        $@"PA2\*{lane}\*([0-9]+)\*([0-9]+)",
                        RegexOptions.Multiline);

                    if (vendMatch.Success)
                    {
                        numberOfVends = int.Parse(vendMatch.Groups[1].Value);
                        valueOfPaidSales = decimal.Parse(vendMatch.Groups[2].Value) / 100m;
                    }

                    string productId = m.Groups[1].Value;
                    dexLaneMeters.Add(new DEXLaneMeterModel(dexMeterId, int.Parse(productId), price, numberOfVends, valueOfPaidSales));
                }

                await _dexRepository.SaveDEXLaneMetersAsync(dexLaneMeters);
                await _dexRepository.CommitTransactionAsync();
            }
            catch (Exception)
            {
                await _dexRepository.RollbackTransactionAsync();
                throw;
            }
        }
    }
}
