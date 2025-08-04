namespace BTM.Products.Application.Queries.GetProducts
{
    public class GetProductResponse
    {
        public int Id { get; }
        public string Name { get; }

        public decimal Price { get; }
        public GetProductResponse(int id, string name, decimal price)
        {
            Id = id;
            Name = name;
            Price = price;
        }
    }
}
