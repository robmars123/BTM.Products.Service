using BTM.Products.Api.Endpoints;
using BTM.Products.Application.Abstractions;
using BTM.Products.Application.Results;
using FluentAssertions;
using Microsoft.AspNetCore.Http.HttpResults;
using Moq;
using System.Net;
using System.Net.Http.Json;

namespace BTM.Endpoints.Tests
{
    public class ProductApiTests : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _client;

        public ProductApiTests(CustomWebApplicationFactory factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetProduct_ReturnsOk()
        {
            var response = await _client.GetAsync("/api/products?id=1");
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task CreateProduct_ReturnsCreated()
        {
            var product = new { Name = "Test", Price = 99.99 };
            var response = await _client.PostAsJsonAsync("/api/products", product);
            response.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        [Fact]
        public async Task RequestToken_ReturnsAccessToken()
        {
            var mockTokenService = new Mock<ITokenService>();
            mockTokenService.Setup(x => x.RequestClientCredentialsTokenAsync())
                .ReturnsAsync(new TokenResult(true, "mock-token", null));

            var endpoints = new ProductEndpoints(mockTokenService.Object);
            var result = await endpoints.RequestToken();

            var okResult = Assert.IsType<Ok<string>>(result);
            Assert.Equal("mock-token", okResult.Value);
        }

    }

}
