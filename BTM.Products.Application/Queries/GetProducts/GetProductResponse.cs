namespace BTM.Products.Application.Queries.GetProducts
{
    public class GetProductResponse
    {
        public int Id { get; }
        public string Name { get; }

        public decimal UnitPrice { get; }
        public GetProductResponse(int id, string name, decimal unitPrice)
        {
            Id = id;
            Name = name;
            UnitPrice = unitPrice;
        }
    }
}
