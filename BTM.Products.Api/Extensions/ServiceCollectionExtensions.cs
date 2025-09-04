using Microsoft.OpenApi.Models;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using BTM.Products.Infrastructure.DependencyInjection;
using BTM.Products.Infrastructure.Connection;
using Microsoft.EntityFrameworkCore;
using BTM.Products.Api.Factories.Abstractions;
using BTM.Products.Api.Factories;
using BTM.Products.Api.Services;
using BTM.Products.Application.Abstractions;
using BTM.Products.Api.Endpoints.Create;

namespace BTM.Products.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCustomServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IProductFactory, ProductFactory>();
            services.AddScoped<CreateProductEndpoints>();
            services.AddScoped<ITokenService, TokenService>();

           // services.AddControllers();
            services.AddInfrastructure();
            services.AddHttpContextAccessor();

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"),
                    sqlOptions =>
                    {
                        sqlOptions.MigrationsAssembly("BTM.Account.Infrastructure");
                        sqlOptions.EnableRetryOnFailure();  // Enables transient failure retry
                    });
            });

            // Ensure the required package is installed: Microsoft.Extensions.Caching.StackExchangeRedis
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = configuration["RedisSettings:ConnectionString"];
                options.InstanceName = "BTMAccount.Cache";
            });

            services.AddOpenApi();
            services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Please enter 'Bearer' followed by your token"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            new string[] {}
                        }
                });
            });

            JsonWebTokenHandler.DefaultInboundClaimTypeMap.Clear();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Authority = "https://localhost:5001";
                    options.Audience = "ProductsAPI";
                    options.TokenValidationParameters = new()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        NameClaimType = "name",
                        RoleClaimType = "role",
                        ValidTypes = new[] { "at+jwt" }
                    };
                    options.Events = new JwtBearerEvents
                    {
                        OnChallenge = context =>
                        {
                            context.HandleResponse();
                            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                            context.Response.ContentType = "application/json";
                            var message = new { error = "Unauthorized", message = "Please click 'Authorize' in Swagger and provide a Bearer token." };
                            return context.Response.WriteAsJsonAsync(message);
                        }
                    };
                });

            services.AddAuthorization();

            return services;
        }
    }
}
