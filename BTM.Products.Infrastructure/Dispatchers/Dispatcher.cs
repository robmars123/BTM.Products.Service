using BTM.Products.Application.Abstractions;
using BTM.Products.Application.Abstractions.Mediator;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Concurrent;

namespace BTM.Products.Infrastructure.Dispatchers;

public class Dispatcher : IDispatcher
{
    private readonly IServiceProvider _serviceProvider;

    private static readonly ConcurrentDictionary<Type, Type> CommandHandlerTypes = new();
    private static readonly ConcurrentDictionary<(Type, Type), Type> RequestHandlerTypes = new();

    public Dispatcher(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task Send<TCommand>(TCommand command) where TCommand : ICommand
    {
        var commandType = typeof(TCommand);
        var handlerType = CommandHandlerTypes.GetOrAdd(commandType,
            t => typeof(ICommandHandler<>).MakeGenericType(t));

        var handler = _serviceProvider.GetService(handlerType);

        if (handler is null)
        {
            throw new InvalidOperationException($"Handler not found for command type: {commandType.Name}");
        }

        var handleMethod = handlerType.GetMethod("Handle");
        if (handleMethod is null)
        {
            throw new InvalidOperationException($"Handle method not found on handler for type: {commandType.Name}");
        }

        await (Task)handleMethod.Invoke(handler, new object[] { command });
    }

    public async Task<TResult> Send<TRequest, TResult>(TRequest request, CancellationToken cancellationToken = default)
        where TRequest : IRequest<TResult>
    {
        var requestType = typeof(TRequest);
        var resultType = typeof(TResult);
        var handlerType = RequestHandlerTypes.GetOrAdd((requestType, resultType),
            _ => typeof(IRequestHandler<,>).MakeGenericType(requestType, resultType));

        var handler = _serviceProvider.GetService(handlerType)
            ?? throw new InvalidOperationException($"Handler not found for request type: {requestType.Name}");

        var handleMethod = handlerType.GetMethod("Handle")
            ?? throw new InvalidOperationException($"Handle method not found on handler for type: {requestType.Name}");

        var result = await (Task<TResult>)handleMethod.Invoke(handler, new object[] { request, cancellationToken });
        return result;
    }
}
