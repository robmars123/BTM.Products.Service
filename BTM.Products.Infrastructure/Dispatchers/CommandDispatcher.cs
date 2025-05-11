using BTM.Products.Application.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace BTM.Products.Infrastructure.Dispatchers
{
    public class CommandDispatcher
    {
        private readonly IServiceProvider _serviceProvider;

        public CommandDispatcher(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        // For commands that return a result
        //public async Task<TResult> Send<TResult>(ICommand<TResult> command, CancellationToken cancellationToken = default)
        //{
        //    var handlerType = typeof(ICommandHandler<,>).MakeGenericType(command.GetType(), typeof(TResult));
        //    dynamic handler = _serviceProvider.GetRequiredService(handlerType);
        //    return await handler.Handle((dynamic)command, cancellationToken);
        //}

        // For commands that do NOT return a result
        public async Task Send(ICommand command, CancellationToken cancellationToken = default)
        {
            var handlerType = typeof(ICommandHandler<>).MakeGenericType(command.GetType());
            dynamic handler = _serviceProvider.GetRequiredService(handlerType);
            await handler.Handle((dynamic)command, cancellationToken);
        }
    }
}
