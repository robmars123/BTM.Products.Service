using BTM.Products.Application.Abstractions.Events;
using BTM.Products.Domain.Events;

namespace BTM.Products.Application.EventHandlers
{
    public class SendProductRemovedEmailHandler : IEventHandler<ProductRemovedEvent>
    {
        public async Task Handle(ProductRemovedEvent @event)
        {
            // Pretend to send email
            await Task.Delay(100); // simulate async I/O
            Console.WriteLine($"Email sent: Product '{@event.ProductId}' was removed on {@event.OccurredOn}!");
        }
    }
}
