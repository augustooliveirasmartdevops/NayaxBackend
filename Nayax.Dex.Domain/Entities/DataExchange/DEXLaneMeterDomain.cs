namespace Nayax.Dex.Domain.Entities.DataExchange
{
    public class DEXLaneMeterDomain
    {
        public int Id { get; set; }
        public Guid ProductIdentifier { get; set; }

        public decimal Price { get; set; }

        public int NumberOfVends { get; set; }

        public decimal ValueOfPaidSales { get; set; }

        public DEXLaneMeterDomain(Guid productIdentifier, decimal price, int numberOfVends, decimal valueOfPaidSales)
        {
            ProductIdentifier = productIdentifier;
            Price = price;
            NumberOfVends = numberOfVends;
            ValueOfPaidSales = valueOfPaidSales;
        }
    }
}
