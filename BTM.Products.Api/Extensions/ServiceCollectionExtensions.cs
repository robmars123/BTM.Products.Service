using BTM.Products.Api.Endpoints.Create;
using BTM.Products.Api.Endpoints.GetById;
using BTM.Products.Api.Factories;
using BTM.Products.Api.Factories.Abstractions;
using BTM.Products.Api.Services;
using BTM.Products.Application.Abstractions;
using BTM.Products.Application.Abstractions.Repositories;
using BTM.Products.Domain.Abstractions;
using BTM.Products.Infrastructure.Connection;
using BTM.Products.Infrastructure.DependencyInjection;
using BTM.Products.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.OpenApi.Models;

namespace BTM.Products.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCustomServices(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment env)
        {
            AddFactories(services);
            AddServices(services);
            AddEndpoints(services);

            services.AddInfrastructure(configuration);
            services.AddHttpContextAccessor();
            AddCrossCuttingConcerns(services, configuration, env);

            services.AddAuthorization();

            return services;
        }

        private static void AddEndpoints(IServiceCollection services)
        {
            services.AddScoped<UpdateProductEndpoints>();
            services.AddScoped<GetAllProductsEndpoint>();
            services.AddScoped<RemoveProductByIdEndpoints>();

            services.AddScoped<CreateProductEndpoints>();
            services.AddScoped<GetProductByIdEndpoints>();
            
            services.AddScoped<IProductRepository, ProductRepository>();
        }

        private static void AddCrossCuttingConcerns(IServiceCollection services, IConfiguration configuration, IWebHostEnvironment env)
        {
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

            // Ensure the required package is installed: Microsoft.Extensions.Caching.StackExchangeRedis
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = configuration["RedisSettings:ConnectionString"];
                options.InstanceName = "BTMProducts.Cache";
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
            services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", options =>
                {
                     options.Authority = "https://host.docker.internal:5001"; //docker internal for local dev
                   // options.Authority = "https://localhost:5001";//local dev
                   // options.Authority = "https://identityserver:443"; // production
                    options.Audience = "ProductsAPI";

                    options.RequireHttpsMetadata = false;

                    options.TokenValidationParameters = new()
                    {
                        ValidateIssuer = true,
                        ValidIssuer = "https://localhost:5001",  // match token's 'iss'
                        ValidateAudience = true,
                        ValidAudience = "ProductsAPI",
                        ValidateLifetime = true,
                        NameClaimType = "name",
                        RoleClaimType = "role"
                    };

                    options.Events = new JwtBearerEvents
                    {
                        OnChallenge = context =>
                        {
                            context.HandleResponse();
                            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                            context.Response.ContentType = "application/json";
                            var message = new { error = "Unauthorized", message = "Unauthorized" };
                            return context.Response.WriteAsJsonAsync(message);
                        }
                    };
                });
        }

        private static void AddServices(IServiceCollection services)
        {
            services.AddScoped<ITokenService, TokenService>();
        }

        private static void AddFactories(IServiceCollection services)
        {
            services.AddTransient<IGetProductByIdFactory, GetProductByIdFactory>();
            services.AddTransient<IGetAllProductsFactory, GetAllProductsFactory>();
        }
    }
}
