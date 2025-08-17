using BTM.Products.Api.DispatcherHandlerDependencies;
using BTM.Products.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.AddServiceDefaults();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCustomServices(builder.Configuration);

RegisterHandlers.AddQueryHandlers(builder);
RegisterHandlers.AddCommandHandlers(builder);
builder.Services.AddOpenApi();

var app = builder.Build();

app.UseCustomMiddlewares();

app.MapProductEndpoints();

if (!app.Environment.IsEnvironment("Testing"))
{
    app.UseAuthentication();
    app.UseAuthorization();
}

app.Run();

namespace BTM.Products.Api
{
    public partial class Program { }
}