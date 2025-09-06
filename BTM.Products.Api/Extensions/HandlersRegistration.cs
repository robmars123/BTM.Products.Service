using BTM.Products.Application.Abstractions;
using BTM.Products.Application.Abstractions.Events;
namespace BTM.Products.Api.Extensions
{
    public static class HandlersRegistration
    {
        public static IServiceCollection RegisterHandlers(this IServiceCollection services)
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies()
                      .Where(a => a.FullName.StartsWith("BTM.Products"))
                      .ToArray();
            services.Scan(scan => scan
                    .FromAssemblies(assemblies)
                    .AddClasses(classes => classes.AssignableTo(typeof(ICommandHandler<>)))
                        .AsImplementedInterfaces()
                        .WithTransientLifetime()
                    .AddClasses(classes => classes.AssignableTo(typeof(IEventHandler<>)))
                        .AsImplementedInterfaces()
                        .WithTransientLifetime()
                    .AddClasses(classes => classes.AssignableTo(typeof(IRequestHandler<,>)))
                        .AsImplementedInterfaces()
                        .WithTransientLifetime()
                );

            return services;
        }
    }
}
