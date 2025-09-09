using BTM.Products.Application.Abstractions;
using BTM.Products.Application.Abstractions.Mediator;
using BTM.Products.Application.Abstractions.Repositories;
using BTM.Products.Domain.Abstractions;
using BTM.Products.Domain.Entities;
using BTM.Products.Domain.Events;

namespace BTM.Products.Application.Commands.AddProduct
{
    public class AddProductCommandHandler : ICommandHandler<AddProductCommand>
    {
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDispatcher _dispatcher;

        public AddProductCommandHandler(IProductRepository productRepository, IUnitOfWork unitOfWork, IDispatcher dispatcher)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
            _dispatcher = dispatcher;
        }
        public async Task Handle(AddProductCommand command, CancellationToken cancellationToken)
        {
            //Create domain entity via factory to enforce business rules
            Product product = Product.Create(command.Name, command.Price);

            await _productRepository.AddProductAsync(product, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            await _dispatcher.Publish(new ProductAddedEvent(product.Id, product.Name, command.Price));
        }
    }
}
