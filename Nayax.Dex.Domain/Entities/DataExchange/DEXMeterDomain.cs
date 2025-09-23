namespace Nayax.Dex.Domain.Entities.DataExchange
{
    public class DEXMeterDomain
    {
        public int Id { get; set; }
        public required string MachineId { get; set; }

        public DateTime DEXDateTime { get; private set; }

        public required string MachineSerialNumber { get; set; }

        public decimal ValueOfPaidVends { get; private set; }

        protected DEXMeterDomain() { }

        public DEXMeterDomain(string machineId, DateTime dexDateTime, string machineSerialNumber, decimal valueOfPaidVends)
        {
            MachineId = machineId;
            DEXDateTime = dexDateTime;
            MachineSerialNumber = machineSerialNumber;
            ValueOfPaidVends = valueOfPaidVends;
        }
    }


}
