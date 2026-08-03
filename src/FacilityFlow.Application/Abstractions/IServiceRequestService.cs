using FacilityFlow.Application.Models;
namespace FacilityFlow.Application.Abstractions;
public interface IServiceRequestService
{
    Task<PagedResult<ServiceRequestDto>> GetAsync(ServiceRequestQuery query,CancellationToken cancellationToken);
    Task<ServiceRequestDto?> GetByIdAsync(Guid id,CancellationToken cancellationToken);
    Task<ServiceRequestDto> CreateAsync(CreateServiceRequestRequest request,CancellationToken cancellationToken);
    Task<ServiceRequestDto?> UpdateAsync(Guid id,UpdateServiceRequestRequest request,CancellationToken cancellationToken);
    Task<bool> DeleteAsync(Guid id,CancellationToken cancellationToken);
    Task<DashboardDto> GetDashboardAsync(CancellationToken cancellationToken);
    Task<IReadOnlyList<BuildingDto>> GetBuildingsAsync(CancellationToken cancellationToken);
}
