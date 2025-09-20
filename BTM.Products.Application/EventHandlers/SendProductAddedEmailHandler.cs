using BTM.Products.Application.Abstractions.Events;
using BTM.Products.Domain.Events;

namespace BTM.Products.Application.EventHandlers
{
    public class SendProductAddedEmailHandler : IEventHandler<ProductAddedEvent>
    {
        public async Task Handle(ProductAddedEvent @event)
        {
            // Pretend to send email
            await Task.Delay(100); // simulate async I/O
            Console.WriteLine($"Email sent: Product '{@event.ProductName}' was added for {@event.UnitPrice:C} on {@event.OccurredOn}!");
        }
    }
}
