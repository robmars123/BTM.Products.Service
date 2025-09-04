namespace BTM.Products.Domain.Entities
{
    public class Product : Entity<int>
    {
        public string Name { get; set; }
        public decimal UnitPrice { get; set; }

        public Product(int id, string name, decimal unitPrice)
        {
            Id = id;
            Name = name;
            UnitPrice = unitPrice;
        }
    }
}