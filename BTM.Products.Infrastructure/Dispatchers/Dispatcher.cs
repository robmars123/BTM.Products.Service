using BTM.Products.Application.Abstractions;
using BTM.Products.Application.Abstractions.Events;
using BTM.Products.Application.Abstractions.Mediator;
using BTM.Products.Domain.Abstractions.Events;
using Microsoft.Extensions.DependencyInjection;

namespace BTM.Products.Infrastructure.Dispatchers;

public class Dispatcher : IDispatcher
{
    private readonly IServiceProvider _serviceProvider;

    public Dispatcher(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    // Command: single handler
    public async Task Send<TCommand>(TCommand command, CancellationToken ct = default)
        where TCommand : ICommand
    {
        var handler = _serviceProvider.GetRequiredService<ICommandHandler<TCommand>>();
        await handler.Handle(command, ct);
    }

    // Query/Request: single handler returning a result
    public async Task<TResult> Send<TRequest, TResult>(TRequest request, CancellationToken ct = default)
        where TRequest : IRequest<TResult>
    {
        var handler = _serviceProvider.GetRequiredService<IRequestHandler<TRequest, TResult>>();
        return await handler.Handle(request, ct);
    }

    // Event: multiple subscribers
    public async Task Publish<TEvent>(TEvent @event) where TEvent : IDomainEvent
    {
        var handlers = _serviceProvider.GetServices<IEventHandler<TEvent>>();
        foreach (var handler in handlers)
        {
            await handler.Handle(@event);
        }
    }
}

