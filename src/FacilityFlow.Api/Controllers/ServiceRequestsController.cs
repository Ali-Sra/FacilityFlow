using FacilityFlow.Application.Abstractions;
using FacilityFlow.Application.Models;
using FacilityFlow.Domain.Enums;
using Microsoft.AspNetCore.Mvc;
namespace FacilityFlow.Api.Controllers;
[ApiController][Route("api/service-requests")]
public sealed class ServiceRequestsController(IServiceRequestService service):ControllerBase
{
 [HttpGet] public Task<PagedResult<ServiceRequestDto>> Get([FromQuery]string? search,[FromQuery]ServiceCategory? category,[FromQuery]Priority? priority,[FromQuery]ServiceRequestStatus? status,[FromQuery]Guid? buildingId,[FromQuery]bool? isOverdue,[FromQuery]int pageNumber=1,[FromQuery]int pageSize=20,[FromQuery]string sortBy="createdAt",[FromQuery]string sortDirection="desc",CancellationToken ct=default)=>service.GetAsync(new(search,category,priority,status,buildingId,isOverdue,pageNumber,pageSize,sortBy,sortDirection),ct);
 [HttpGet("{id:guid}")] public async Task<ActionResult<ServiceRequestDto>> GetById(Guid id,CancellationToken ct){var item=await service.GetByIdAsync(id,ct);return item is null?NotFound():Ok(item);}
 [HttpPost] public async Task<ActionResult<ServiceRequestDto>> Create(CreateServiceRequestRequest request,CancellationToken ct){var item=await service.CreateAsync(request,ct);return CreatedAtAction(nameof(GetById),new{id=item.Id},item);}
 [HttpPut("{id:guid}")] public async Task<ActionResult<ServiceRequestDto>> Update(Guid id,UpdateServiceRequestRequest request,CancellationToken ct){var item=await service.UpdateAsync(id,request,ct);return item is null?NotFound():Ok(item);}
 [HttpDelete("{id:guid}")] public async Task<IActionResult> Delete(Guid id,CancellationToken ct)=>await service.DeleteAsync(id,ct)?NoContent():NotFound();
 [HttpGet("dashboard")] public Task<DashboardDto> Dashboard(CancellationToken ct)=>service.GetDashboardAsync(ct);
 [HttpGet("buildings")] public Task<IReadOnlyList<BuildingDto>> Buildings(CancellationToken ct)=>service.GetBuildingsAsync(ct);
}
