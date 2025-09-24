namespace Nayax.Dex.CrossCutting.Models
{
    public class DEXMeterModel
    {
        public int Id { get; set; }
        public string MachineId { get; set; }

        public DateTime DEXDateTime { get; set; }

        public string MachineSerialNumber { get; set; }

        public decimal ValueOfPaidVends { get; set; }

        public DEXMeterModel(string machineId, DateTime dexDateTime, string machineSerialNumber, decimal valueOfPaidVends)
        {
            MachineId = machineId;
            DEXDateTime = dexDateTime;
            MachineSerialNumber = machineSerialNumber;
            ValueOfPaidVends = valueOfPaidVends;
        }
    }
}
