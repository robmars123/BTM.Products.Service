using BTM.Products.Application.Abstractions.Events;
using BTM.Products.Domain.Events;

namespace BTM.Products.Application.EventHandlers
{
    public class SendProductUpdatedEmailHandler : IEventHandler<ProductUpdatedEvent>
    {
        public async Task Handle(ProductUpdatedEvent @event)
        {
            // Pretend to send email
            await Task.Delay(100); // simulate async I/O
            Console.WriteLine($"Email sent: Product '{@event.Id}' was updated on {@event.OccurredOn}!");
        }
    }
}
