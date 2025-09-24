namespace Nayax.Dex.Domain.Entities.DataExchange
{
    public class DEXLaneMeterDomain
    {
        public int Id { get; set; }

        public int ProductIdentifier { get; set; }

        public decimal Price { get; set; }

        public int NumberOfVends { get; set; }

        public decimal ValueOfPaidSales { get; set; }

        public DEXLaneMeterDomain(int productIdentifier, decimal price, int numberOfVends, decimal valueOfPaidSales)
        {
            ProductIdentifier = productIdentifier;
            Price = price;
            NumberOfVends = numberOfVends;
            ValueOfPaidSales = valueOfPaidSales;
        }
    }
}
