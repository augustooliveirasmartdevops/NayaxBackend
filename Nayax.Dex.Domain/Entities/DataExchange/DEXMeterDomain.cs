namespace Nayax.Dex.Domain.Entities.DataExchange
{
    public class DEXMeterDomain
    {
        public int ID { get; private set; }

        public required string MachineId { get; set; }

        public DateTime DEXDateTime { get; private set; }

        public required string MachineSerialNumber { get; set; }

        public decimal ValueOfPaidVends { get; private set; }

        //protected DEXMeterDomain() { }

        //public DEXMeterDomain(int id, string machineId, DateTime dexDateTime, string machineSerialNumber, decimal valueOfPaidVends)
        //{
        //    ID = id;
        //    MachineId = machineId;
        //    DEXDateTime = dexDateTime;
        //    MachineSerialNumber = machineSerialNumber;
        //    ValueOfPaidVends = valueOfPaidVends;
        //}
    }


}
