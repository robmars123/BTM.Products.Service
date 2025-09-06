using BTM.Products.Domain.Abstractions.Events;

namespace BTM.Products.Application.Abstractions.Events
{
    public interface IEventHandler<TEvent> where TEvent : IDomainEvent
    {
        Task Handle(TEvent @event);
    }
}
