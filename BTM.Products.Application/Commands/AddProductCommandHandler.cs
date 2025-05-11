using BTM.Products.Application.Abstractions;
using BTM.Products.Domain.Entities;

namespace BTM.Products.Application.Commands
{
    public class AddProductCommandHandler : ICommandHandler<AddProductCommand>
    {
        public Task Handle(AddProductCommand command, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
