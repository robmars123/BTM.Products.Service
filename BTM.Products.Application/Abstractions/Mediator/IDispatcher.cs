using BTM.Products.Domain.Abstractions.Events;

namespace BTM.Products.Application.Abstractions.Mediator
{
    public interface IDispatcher
    {
        Task Send<TCommand>(TCommand command, CancellationToken cancellationToken = default) where TCommand : ICommand;
        Task<TResult> Send<TRequest, TResult>(TRequest request, CancellationToken cancellationToken) where TRequest : IRequest<TResult>;
        Task Publish<TEvent>(TEvent @event) where TEvent : IDomainEvent;
    }

}
