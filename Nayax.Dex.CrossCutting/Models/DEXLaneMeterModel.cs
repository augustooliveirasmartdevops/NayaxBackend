namespace Nayax.Dex.CrossCutting.Models
{
    public class DEXLaneMeterModel
    {
        public int DEXMeterId { get; set; }

        public Guid ProductIdentifier { get; set; }

        public decimal Price { get; set; }

        public int NumberOfVends { get; set; }

        public decimal ValueOfPaidSales { get; set; }

        public DEXLaneMeterModel(int dexMeterId, Guid productIdentifier, decimal price, int numberOfVends, decimal valueOfPaidSales)
        {
            DEXMeterId = dexMeterId;
            ProductIdentifier = productIdentifier;
            Price = price;
            NumberOfVends = numberOfVends;
            ValueOfPaidSales = valueOfPaidSales;
        }
    }
}
