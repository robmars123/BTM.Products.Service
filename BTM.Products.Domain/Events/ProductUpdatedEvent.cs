using BTM.Products.Domain.Abstractions.Events;

namespace BTM.Products.Domain.Events
{
    public class ProductUpdatedEvent : IDomainEvent
    {
        public Guid Id { get; }
        public string Name { get; }
        public decimal UnitPrice { get; }

        public DateTime OccurredOn { get; }

        public ProductUpdatedEvent(Guid productId, string name, decimal unitPrice)
        {
            Id = productId;
            OccurredOn = DateTime.UtcNow;
            Name = name;
            UnitPrice = unitPrice;
        }
    }
}
