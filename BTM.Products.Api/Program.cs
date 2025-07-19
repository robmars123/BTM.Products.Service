using BTM.Products.Api.DispatcherHandlerDependencies;
using BTM.Products.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.AddServiceDefaults();

builder.Services.AddCustomServices(builder.Configuration);

RegisterHandlers.AddRequestHandlers(builder);
RegisterHandlers.AddCommandHandlers(builder);
builder.Services.AddOpenApi();

var app = builder.Build();

app.UseCustomMiddlewares();
app.Run();

