namespace BTM.Products.Application.Queries.GetAllProducts
{
    public class GetAllProductsResponse
    {
        public int Id { get; }
        public string Name { get; }

        public decimal UnitPrice { get; }
        public GetAllProductsResponse(int id, string name, decimal unitPrice)
        {
            Id = id;
            Name = name;
            UnitPrice = unitPrice;
        }
    }
}
