namespace BTM.Products.ApiClient.Out
{
    public class ProductResponse
    {
        public int Id { get; }
        public string Name { get; }
        public decimal UnitPrice { get; }
        public ProductResponse(int id, string name, decimal unitPrice)
        {
            Id = id;
            Name = name;
            UnitPrice = unitPrice;
        }
    }
}
