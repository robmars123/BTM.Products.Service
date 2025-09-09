using BTM.Products.Application.Abstractions;

namespace BTM.Products.Application.Commands.DeleteProduct
{
    public class RemoveProductCommand : ICommand
    {
        public Guid Id { get; }
        public RemoveProductCommand(Guid Id)
        {
            this.Id = Id;
        }
    }
}
