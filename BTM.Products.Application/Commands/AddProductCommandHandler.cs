using BTM.Products.Application.Abstractions;
using BTM.Products.Application.Abstractions.Repositories;
using BTM.Products.Domain.Abstractions;
using BTM.Products.Domain.Entities;

namespace BTM.Products.Application.Commands
{
    public class AddProductCommandHandler : ICommandHandler<AddProductCommand>
    {
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AddProductCommandHandler(IProductRepository productRepository, IUnitOfWork unitOfWork)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task Handle(AddProductCommand command, CancellationToken cancellationToken)
        {
            //Create domain entity via factory to enforce business rules
            var product = Product.Create(command.Name, command.Price);

            await _productRepository.AddProductAsync(product, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
