namespace BTM.Products.Domain.Entities
{
    public class Product : Entity 
    {
        public string Name { get; private set; }
        public decimal UnitPrice { get; private set; }
        public bool IsDeleted { get; private set; } = false;
        public DateTime CreatedDate { get; private set; }

        public Product(Guid id, string name, decimal unitPrice, DateTime createdDate) : base(id)
        {
            Name = name;
            UnitPrice = unitPrice;
            CreatedDate = createdDate;
        }

        public static Product Create(string name, decimal price, DateTime createdDate)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Product name cannot be empty.", nameof(name));

            if (price < 0)
                throw new ArgumentException("Product price must be greater than or equal to zero.", nameof(price));

            var id = Guid.NewGuid();
            return new Product(id, name, price, createdDate);
        }

        public void UpdateProduct(string name, decimal newPrice, bool isDeleted)
        {
            if (newPrice < 0)
                throw new ArgumentException("Price must be greater than or equal to zero.", nameof(newPrice));

            Name = name;
            UnitPrice = newPrice;
            IsDeleted = isDeleted;
        }

        public void Remove()
        {
            IsDeleted = true;
        }
    }
}