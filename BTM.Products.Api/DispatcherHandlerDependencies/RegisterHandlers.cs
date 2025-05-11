using BTM.Products.Application.Abstractions;

namespace BTM.Products.Api.DispatcherHandlerDependencies
{
    public static class RegisterHandlers
    {
        /// <summary>
        /// Registers all request handlers in the specified assembly.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="assembly"></param>
        public static void AddRequestHandlers(this WebApplicationBuilder builder)
        {
            var handlerInterfaceType = typeof(IRequestHandler<,>);

            var handlerTypes = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(a => a.GetTypes())
                .Where(t => t.IsClass && !t.IsAbstract)
                .SelectMany(t => t.GetInterfaces()
                    .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == handlerInterfaceType)
                    .Select(i => new { Interface = i, Implementation = t }));

            foreach (var handler in handlerTypes)
            {
                builder.Services.AddTransient(handler.Interface, handler.Implementation);
            }
        }

        public static void AddCommandHandlers(this WebApplicationBuilder builder)
        {
            var handlerInterfaceType = typeof(ICommandHandler<>);

            var handlerTypes = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(a => a.GetTypes())
                .Where(t => t.IsClass && !t.IsAbstract)
                .SelectMany(t => t.GetInterfaces()
                    .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == handlerInterfaceType)
                    .Select(i => new { Interface = i, Implementation = t }));

            foreach (var handler in handlerTypes)
            {
                builder.Services.AddTransient(handler.Interface, handler.Implementation);
            }
        }
    }
}
