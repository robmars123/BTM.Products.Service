using BTM.Products.Application.Abstractions.Mediator;
using BTM.Products.Application.Abstractions.Repositories;
using BTM.Products.Domain.Abstractions;
using BTM.Products.Infrastructure.Connection;
using BTM.Products.Infrastructure.Dispatchers;
using BTM.Products.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BTM.Products.Infrastructure.DependencyInjection
{
    public static class RegisterDependencies
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // Register other infra services like repositories, etc.
            services.AddScoped<IDispatcher, Dispatcher>(); // Register Dispatcher service

            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IUnitOfWork>(provider => provider.GetRequiredService<ApplicationDbContext>());

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"),
                    sqlOptions =>
                    {
                        sqlOptions.MigrationsAssembly("BTM.Products.Infrastructure");
                        sqlOptions.EnableRetryOnFailure();  // Enables transient failure retry
                    });
            });

            return services;
        }
    }
}
