namespace BTM.Products.Domain.Entities
{
    public class Product : Entity 
    {
        public string Name { get; set; }
        public decimal UnitPrice { get; set; }

        public Product(Guid id, string name, decimal unitPrice) : base(id)
        {
            Name = name;
            UnitPrice = unitPrice;
        }

        public static Product Create(string name, decimal price)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Product name cannot be empty.", nameof(name));

            if (price < 0)
                throw new ArgumentException("Product price must be greater than or equal to zero.", nameof(price));

            var id = Guid.NewGuid();
            return new Product(id, name, price);
        }

        public void UpdatePrice(decimal newPrice)
        {
            if (newPrice < 0)
                throw new ArgumentException("Price must be greater than or equal to zero.", nameof(newPrice));

            UnitPrice = newPrice;
        }
    }
}