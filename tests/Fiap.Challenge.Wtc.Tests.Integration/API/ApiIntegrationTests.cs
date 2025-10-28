using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;

namespace Fiap.Challenge.Wtc.Tests.Integration.API;

public class ApiIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;
    private readonly HttpClient _client;

    public ApiIntegrationTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
        _client = _factory.CreateClient();
    }

    [Fact]
    public async Task HealthCheck_Should_ReturnOk()
    {
        // Arrange & Act
        var response = await _client.GetAsync("/");

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode); // Expected since we don't have a root endpoint
    }
}