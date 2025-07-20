using BTM.Products.Application.Abstractions.Mediator;
using BTM.Products.Infrastructure.Dispatchers;
using Microsoft.Extensions.DependencyInjection;

namespace BTM.Products.Infrastructure.DependencyInjection
{
    public static class RegisterDependencies
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            // Register other infra services like repositories, etc.
            services.AddScoped<IDispatcher, Dispatcher>(); // Register Dispatcher service

            return services;
        }
    }
}
