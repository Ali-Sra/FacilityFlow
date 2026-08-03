using Microsoft.AspNetCore.Mvc.Testing;
namespace FacilityFlow.IntegrationTests;
public sealed class HealthEndpointTests : IClassFixture<WebApplicationFactory<Program>>
{
 private readonly HttpClient client;public HealthEndpointTests(WebApplicationFactory<Program> factory)=>client=factory.CreateClient();
 [Fact] public async Task Health_ReturnsSuccess(){var response=await client.GetAsync("/health");response.EnsureSuccessStatusCode();}
}
