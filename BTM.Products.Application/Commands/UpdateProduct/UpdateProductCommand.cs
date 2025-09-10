using BTM.Products.Application.Abstractions;

namespace BTM.Products.Application.Commands.AddProduct
{
    public class UpdateProductCommand : ICommand
    {
        public Guid Id { get; }
        public string Name { get; }
        public decimal UnitPrice { get; }

        public bool IsDeleted { get; set; }
        public UpdateProductCommand(Guid id, string name, decimal unitPrice, bool isDeleted)
        {
            Id = id;
            Name = name;
            UnitPrice = unitPrice;
            IsDeleted = isDeleted;
        }
    }
}
