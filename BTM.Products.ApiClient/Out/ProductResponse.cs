namespace BTM.Products.ApiClient.Out
{
    public class ProductResponse
    {
        public Guid Id { get; }
        public string Name { get; }
        public decimal UnitPrice { get; }
        public ProductResponse(Guid id, string name, decimal unitPrice)
        {
            Id = id;
            Name = name;
            UnitPrice = unitPrice;
        }
    }
}
