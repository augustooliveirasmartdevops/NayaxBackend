namespace Nayax.Dex.Domain.Entities.DataExchange
{
    public class DEXLaneMeterDomain
    {
        public int Id { get; set; }
        public Guid ProductIdentifier { get; private set; }

        public decimal Price { get; private set; }

        public int NumberOfVends { get; private set; }

        public decimal ValueOfPaidSales { get; private set; }

        public DEXLaneMeterDomain(int id, Guid productIdentifier, decimal price, int numberOfVends, decimal valueOfPaidSales)
        {
            Id = id;
            ProductIdentifier = productIdentifier;
            Price = price;
            NumberOfVends = numberOfVends;
            ValueOfPaidSales = valueOfPaidSales;
        }
    }
}
