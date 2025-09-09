using BTM.Products.Application.Abstractions;

namespace BTM.Products.Application.Commands.AddProduct
{
    public class AddProductCommand : ICommand
    {
        public string Name { get; }
        public decimal Price { get; }

        public AddProductCommand(string name, decimal price)
        {
            Name = name;
            Price = price;
        }
    }
}
