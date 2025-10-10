using BTM.Products.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.AddServiceDefaults();

// Add CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularDevClient", policy =>
    {
        policy.WithOrigins("http://localhost:4200") //Specific origin
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials(); //Only needed if you're using cookies or credentials
    });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCustomServices(builder.Configuration, builder.Environment);

//Register all command, event and request handlers automatically
builder.Services.RegisterHandlers();

builder.Services.AddOpenApi();

var app = builder.Build();

app.UseCustomMiddlewares();

app.MapProductEndpoints();

// Use CORS
app.UseCors("AllowAngularDevClient");

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