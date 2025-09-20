using BTM.Products.Application.Abstractions.Mediator;
using BTM.Products.Infrastructure.Dispatchers;
using Microsoft.Extensions.DependencyInjection;

namespace BTM.Products.Composition
{
    public static class CompositionRegistries
    {
        // Method to register all necessary services in the DI container
        public static IServiceCollection AddCompositionServices(this IServiceCollection services)
        {
            // Register infrastructure services like Dispatcher
            services.AddScoped<IDispatcher, Dispatcher>(); // Register Dispatcher service

            return services;
        }
    }
}
