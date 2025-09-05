using BTM.Products.Application.Abstractions;
using BTM.Products.Application.Abstractions.Repositories;
using BTM.Products.Domain.Entities;

namespace BTM.Products.Application.Commands
{
    public class AddProductCommandHandler : ICommandHandler<AddProductCommand>
    {
        private readonly IProductRepository _productRepository;

        public AddProductCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task Handle(AddProductCommand command, CancellationToken cancellationToken)
        {
            // 1️⃣ Create domain entity via factory to enforce business rules
            var product = Product.Create(command.Name, command.Price);

            await _productRepository.AddProductAsync(product, cancellationToken);

            //todo: implement unit of work
            // await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
