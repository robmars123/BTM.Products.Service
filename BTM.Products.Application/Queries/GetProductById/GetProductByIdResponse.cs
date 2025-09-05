namespace BTM.Products.Application.Queries.GetProducts
{
    public class GetProductByIdResponse
    {
        public Guid Id { get; }
        public string Name { get; }

        public decimal UnitPrice { get; }
        public GetProductByIdResponse(Guid id, string name, decimal unitPrice)
        {
            Id = id;
            Name = name;
            UnitPrice = unitPrice;
        }
    }
}
