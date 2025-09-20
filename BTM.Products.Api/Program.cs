using BTM.Products.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.AddServiceDefaults();

// Add CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularDevClient", policy =>
    {
        policy.AllowAnyOrigin() // Angular dev server
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCustomServices(builder.Configuration);

//Register all command, event and request handlers automatically
builder.Services.RegisterHandlers();

builder.Services.AddOpenApi();

var app = builder.Build();

app.UseCustomMiddlewares();

app.MapProductEndpoints();

if (!app.Environment.IsEnvironment("Testing"))
{
    app.UseAuthentication();
    app.UseAuthorization();
}

// Use CORS
app.UseCors("AllowAngularDevClient");

app.Run();

namespace BTM.Products.Api
{
    public partial class Program { }
}