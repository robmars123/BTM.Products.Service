using BTM.Products.Application.Abstractions;
using BTM.Products.Application.Abstractions.Mediator;
using BTM.Products.Application.Abstractions.Repositories;
using BTM.Products.Application.Commands.AddProduct;
using BTM.Products.Domain.Abstractions;
using BTM.Products.Domain.Entities;
using BTM.Products.Domain.Events;

namespace BTM.Products.Application.Commands.UpdateProduct
{
    public class UpdateProductCommandHandler : ICommandHandler<UpdateProductCommand>
    {
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDispatcher _dispatcher;

        public UpdateProductCommandHandler(IProductRepository productRepository, IUnitOfWork unitOfWork, IDispatcher dispatcher)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
            _dispatcher = dispatcher;
        }
        public async Task Handle(UpdateProductCommand command, CancellationToken cancellationToken)
        {
            Product? product = await _productRepository.GetByIdAsync(command.Id, cancellationToken);

            product.UpdateProduct(command.Name, command.UnitPrice, command.IsDeleted);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            await _dispatcher.Publish(new ProductUpdatedEvent(product.Id, product.Name, command.UnitPrice));
        }
    }
}
