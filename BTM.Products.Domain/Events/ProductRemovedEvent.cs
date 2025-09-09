using BTM.Products.Domain.Abstractions.Events;

namespace BTM.Products.Domain.Events
{
    public class ProductRemovedEvent : IDomainEvent
    {
        public Guid ProductId { get; }

        public DateTime OccurredOn { get; }

        public ProductRemovedEvent(Guid productId)
        {
            ProductId = productId;
            OccurredOn = DateTime.UtcNow;
        }
    }
}
