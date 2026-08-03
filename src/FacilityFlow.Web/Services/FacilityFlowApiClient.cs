using System.Net.Http.Json;
using FacilityFlow.Application.Models;
namespace FacilityFlow.Web.Services;
public sealed class FacilityFlowApiClient(HttpClient http)
{
 public async Task<DashboardDto?> GetDashboardAsync(CancellationToken ct=default)=>await http.GetFromJsonAsync<DashboardDto>("api/service-requests/dashboard",ct);
 public async Task<PagedResult<ServiceRequestDto>?> GetRequestsAsync(string? search=null,CancellationToken ct=default)=>await http.GetFromJsonAsync<PagedResult<ServiceRequestDto>>($"api/service-requests?pageSize=50&search={Uri.EscapeDataString(search??string.Empty)}",ct);
 public async Task<IReadOnlyList<BuildingDto>> GetBuildingsAsync(CancellationToken ct=default)=>await http.GetFromJsonAsync<List<BuildingDto>>("api/service-requests/buildings",ct)??[];
 public async Task<ServiceRequestDto?> CreateAsync(CreateServiceRequestRequest request,CancellationToken ct=default){var response=await http.PostAsJsonAsync("api/service-requests",request,ct);response.EnsureSuccessStatusCode();return await response.Content.ReadFromJsonAsync<ServiceRequestDto>(cancellationToken:ct);}
}
