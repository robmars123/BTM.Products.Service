using BTM.Products.Domain.Abstractions.Events;

namespace BTM.Products.Domain.Events
{
    public class ProductAddedEvent : IDomainEvent
    {
        public Guid ProductId { get; }
        public string ProductName { get; }
        public decimal UnitPrice { get; }
        public DateTime OccurredOn { get; }

        public ProductAddedEvent(Guid productId, string productName, decimal unitPrice)
        {
            ProductId = productId;
            ProductName = productName;
            UnitPrice = unitPrice;
            OccurredOn = DateTime.UtcNow;
        }
    }
}
