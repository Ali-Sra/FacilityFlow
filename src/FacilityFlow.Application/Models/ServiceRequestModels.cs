using FacilityFlow.Domain.Enums;
namespace FacilityFlow.Application.Models;
public sealed record ServiceRequestDto(Guid Id,string TicketNumber,string Title,string Description,ServiceCategory Category,Priority Priority,ServiceRequestStatus Status,Guid BuildingId,string BuildingName,Guid? RoomId,string? RoomNumber,string? AssignedTechnician,DateTime DueDateUtc,DateTime CreatedAtUtc,bool IsOverdue,byte[] RowVersion);
public sealed record CreateServiceRequestRequest(string Title,string Description,ServiceCategory Category,Priority Priority,Guid BuildingId,Guid? RoomId,string? AssignedTechnician,DateTime? DueDateUtc);
public sealed record UpdateServiceRequestRequest(string Title,string Description,ServiceCategory Category,Priority Priority,ServiceRequestStatus Status,Guid BuildingId,Guid? RoomId,string? AssignedTechnician,DateTime DueDateUtc,string? ResolutionDescription,byte[] RowVersion);
public sealed record ServiceRequestQuery(string? Search, ServiceCategory? Category, Priority? Priority, ServiceRequestStatus? Status, Guid? BuildingId, bool? IsOverdue, int PageNumber = 1, int PageSize = 20, string SortBy = "createdAt", string SortDirection = "desc");
public sealed record PagedResult<T>(IReadOnlyList<T> Items,int PageNumber,int PageSize,int TotalCount)
{ public int TotalPages => (int)Math.Ceiling(TotalCount/(double)PageSize); }
public sealed record DashboardDto(int Total,int Open,int Critical,int Overdue,int Resolved,IReadOnlyDictionary<string,int> ByStatus,IReadOnlyDictionary<string,int> ByCategory,IReadOnlyDictionary<string,int> ByBuilding);
public sealed record BuildingDto(Guid Id,string Name,string Code,IReadOnlyList<RoomDto> Rooms);
public sealed record RoomDto(Guid Id,string RoomNumber,string Floor);
