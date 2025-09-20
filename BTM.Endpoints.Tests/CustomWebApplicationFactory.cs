using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using BTM.Products.Api;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace BTM.Endpoints.Tests
{
    public class CustomWebApplicationFactory : WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureTestServices(services =>
            {
                // Remove existing auth handlers
                services.RemoveAll(typeof(AuthenticationSchemeProvider));
                services.RemoveAll(typeof(IAuthenticationHandlerProvider));

                // Add test auth scheme
                services.AddAuthentication("Test")
                        .AddScheme<AuthenticationSchemeOptions, TestAuthHandler>("Test", options => { });

                services.AddSingleton<IAuthenticationSchemeProvider, AuthenticationSchemeProvider>();
            });
        }
    }

}
