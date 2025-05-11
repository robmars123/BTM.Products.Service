using BTM.Products.Application.Abstractions;
using BTM.Products.Application.Abstractions.Mediator;
using Microsoft.Extensions.DependencyInjection;

namespace BTM.Products.Infrastructure.Dispatchers;

public class Dispatcher : IDispatcher
{
    private readonly IServiceProvider _serviceProvider;

    public Dispatcher(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    /// <summary>
    /// Sends a command to the appropriate command handler.
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    public async Task Send(ICommand command)
    {
        var handlerType = typeof(ICommandHandler<>).MakeGenericType(command.GetType());
        dynamic handler = _serviceProvider.GetRequiredService(handlerType);
        await handler.Handle((dynamic)command);
    }

    /// <summary>
    /// Sends a request to the appropriate request handler and returns the result.
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="request"></param>
    /// <returns></returns>
    public async Task<TResult> Send<TResult>(IRequest<TResult> request)
    {
        var handlerType = typeof(IRequestHandler<,>).MakeGenericType(request.GetType(), typeof(TResult));
        dynamic handler = _serviceProvider.GetRequiredService(handlerType);
        return await handler.Handle((dynamic)request);
    }
}
