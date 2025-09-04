namespace BTM.Products.Application.Queries.GetProducts
{
    public class GetProductByIdResponse
    {
        public int Id { get; }
        public string Name { get; }

        public decimal UnitPrice { get; }
        public GetProductByIdResponse(int id, string name, decimal unitPrice)
        {
            Id = id;
            Name = name;
            UnitPrice = unitPrice;
        }
    }
}
