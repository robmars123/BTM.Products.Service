namespace BTM.Products.Application.Queries.GetAllProducts
{
    public class GetAllProductsResponse
    {
        public Guid Id { get; }
        public string Name { get; }

        public decimal UnitPrice { get; }
        public GetAllProductsResponse(Guid id, string name, decimal unitPrice)
        {
            Id = id;
            Name = name;
            UnitPrice = unitPrice;
        }
    }
}
