using BTM.Products.Application.Abstractions;
using BTM.Products.Application.Abstractions.Mediator;
using BTM.Products.Application.Abstractions.Repositories;
using BTM.Products.Application.Commands.DeleteProduct;
using BTM.Products.Domain.Abstractions;
using BTM.Products.Domain.Events;

namespace BTM.Products.Application.Commands.RemoveProduct
{
    public class RemoveProductCommandHandler : ICommandHandler<RemoveProductCommand>
    {
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDispatcher _dispatcher;
        public RemoveProductCommandHandler(IProductRepository productRepository, IUnitOfWork unitOfWork, IDispatcher dispatcher)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
            _dispatcher = dispatcher;
        }

        public async Task Handle(RemoveProductCommand command, CancellationToken cancellationToken)
        {
            await _productRepository.RemoveAsync(command.Id, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            await _dispatcher.Publish(new ProductRemovedEvent(command.Id));
        }
    }
}
