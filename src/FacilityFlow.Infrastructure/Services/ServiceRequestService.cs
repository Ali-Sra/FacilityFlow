using FacilityFlow.Application.Abstractions;
using FacilityFlow.Application.Models;
using FacilityFlow.Domain.Entities;
using FacilityFlow.Domain.Enums;
using FacilityFlow.Domain.Rules;
using FacilityFlow.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
namespace FacilityFlow.Infrastructure.Services;
public sealed class ServiceRequestService(FacilityFlowDbContext db) : IServiceRequestService
{
    public async Task<PagedResult<ServiceRequestDto>> GetAsync(ServiceRequestQuery query,CancellationToken ct)
    {
        var q=db.ServiceRequests.AsNoTracking().Include(x=>x.Building).Include(x=>x.Room).AsQueryable();
        if(!string.IsNullOrWhiteSpace(query.Search)){var s=query.Search.Trim();q=q.Where(x=>x.TicketNumber.Contains(s)||x.Title.Contains(s)||x.Description.Contains(s));}
        if(query.Category.HasValue) q=q.Where(x=>x.Category==query.Category);
        if(query.Priority.HasValue) q=q.Where(x=>x.Priority==query.Priority);
        if(query.Status.HasValue) q=q.Where(x=>x.Status==query.Status);
        if(query.BuildingId.HasValue) q=q.Where(x=>x.BuildingId==query.BuildingId);
        if(query.IsOverdue==true) q=q.Where(x=>x.DueDateUtc<DateTime.UtcNow && x.Status!=ServiceRequestStatus.Geloest && x.Status!=ServiceRequestStatus.Geschlossen);
        q=(query.SortBy.ToLowerInvariant(),query.SortDirection.ToLowerInvariant()) switch
        { ("title","asc")=>q.OrderBy(x=>x.Title),("title",_)=>q.OrderByDescending(x=>x.Title),("duedate","asc")=>q.OrderBy(x=>x.DueDateUtc),("duedate",_)=>q.OrderByDescending(x=>x.DueDateUtc),("priority","asc")=>q.OrderBy(x=>x.Priority),("priority",_)=>q.OrderByDescending(x=>x.Priority),("createdat","asc")=>q.OrderBy(x=>x.CreatedAtUtc),_=>q.OrderByDescending(x=>x.CreatedAtUtc)};
        var page=Math.Max(1,query.PageNumber);var size=Math.Clamp(query.PageSize,1,100);var total=await q.CountAsync(ct);
        var entities=await q.Skip((page-1)*size).Take(size).ToListAsync(ct);
        var items=entities.Select(Map).ToList();
        return new(items,page,size,total);
    }
    public async Task<ServiceRequestDto?> GetByIdAsync(Guid id,CancellationToken ct)=>
        await db.ServiceRequests.AsNoTracking().Include(x=>x.Building).Include(x=>x.Room).Where(x=>x.Id==id).Select(x=>Map(x)).SingleOrDefaultAsync(ct);
    public async Task<ServiceRequestDto> CreateAsync(CreateServiceRequestRequest r,CancellationToken ct)
    {
        Validate(r.Title,r.Description);await ValidateLocation(r.BuildingId,r.RoomId,ct);
        var next=(await db.ServiceRequests.IgnoreQueryFilters().CountAsync(ct))+1;
        var e=new ServiceRequest{TicketNumber=$"SR-{DateTime.UtcNow:yyyy}-{next:000000}",Title=r.Title.Trim(),Description=r.Description.Trim(),Category=r.Category,Priority=r.Priority,BuildingId=r.BuildingId,RoomId=r.RoomId,AssignedTechnician=r.AssignedTechnician,DueDateUtc=r.DueDateUtc??CalculateDueDate(r.Priority)};
        db.Add(e);await db.SaveChangesAsync(ct);return (await GetByIdAsync(e.Id,ct))!;
    }
    public async Task<ServiceRequestDto?> UpdateAsync(Guid id,UpdateServiceRequestRequest r,CancellationToken ct)
    {
        Validate(r.Title,r.Description);await ValidateLocation(r.BuildingId,r.RoomId,ct);
        var e=await db.ServiceRequests.Include(x=>x.Building).Include(x=>x.Room).SingleOrDefaultAsync(x=>x.Id==id,ct);if(e is null)return null;
        if(!StatusTransitionRules.IsAllowed(e.Status,r.Status)) throw new InvalidOperationException($"Der Statuswechsel von {e.Status} nach {r.Status} ist nicht zulässig.");
        if(r.Status==ServiceRequestStatus.Geloest && string.IsNullOrWhiteSpace(r.ResolutionDescription)) throw new ArgumentException("Für gelöste Anfragen ist eine Lösungsbeschreibung erforderlich.");
        db.Entry(e).Property(x=>x.RowVersion).OriginalValue=r.RowVersion;
        e.Title=r.Title.Trim();e.Description=r.Description.Trim();e.Category=r.Category;e.Priority=r.Priority;e.Status=r.Status;e.BuildingId=r.BuildingId;e.RoomId=r.RoomId;e.AssignedTechnician=r.AssignedTechnician;e.DueDateUtc=r.DueDateUtc;e.ResolutionDescription=r.ResolutionDescription;e.UpdatedAtUtc=DateTime.UtcNow;
        if(r.Status==ServiceRequestStatus.Geloest)e.ResolvedAtUtc=DateTime.UtcNow;
        await db.SaveChangesAsync(ct);return await GetByIdAsync(id,ct);
    }
    public async Task<bool> DeleteAsync(Guid id,CancellationToken ct){var e=await db.ServiceRequests.SingleOrDefaultAsync(x=>x.Id==id,ct);if(e is null)return false;e.IsDeleted=true;e.UpdatedAtUtc=DateTime.UtcNow;await db.SaveChangesAsync(ct);return true;}
    public async Task<DashboardDto> GetDashboardAsync(CancellationToken ct)
    {
        var q=db.ServiceRequests.AsNoTracking();var now=DateTime.UtcNow;var total=await q.CountAsync(ct);var open=await q.CountAsync(x=>x.Status!=ServiceRequestStatus.Geloest&&x.Status!=ServiceRequestStatus.Geschlossen&&x.Status!=ServiceRequestStatus.Abgelehnt,ct);var critical=await q.CountAsync(x=>x.Priority==Priority.Kritisch,ct);var overdue=await q.CountAsync(x=>x.DueDateUtc<now&&x.Status!=ServiceRequestStatus.Geloest&&x.Status!=ServiceRequestStatus.Geschlossen,ct);var resolved=await q.CountAsync(x=>x.Status==ServiceRequestStatus.Geloest||x.Status==ServiceRequestStatus.Geschlossen,ct);
        var byStatus=await q.GroupBy(x=>x.Status).ToDictionaryAsync(x=>x.Key.ToString(),x=>x.Count(),ct);var byCategory=await q.GroupBy(x=>x.Category).ToDictionaryAsync(x=>x.Key.ToString(),x=>x.Count(),ct);var byBuilding=await q.GroupBy(x=>x.Building!.Name).ToDictionaryAsync(x=>x.Key,x=>x.Count(),ct);
        return new(total,open,critical,overdue,resolved,byStatus,byCategory,byBuilding);
    }
    public async Task<IReadOnlyList<BuildingDto>> GetBuildingsAsync(CancellationToken ct)=>await db.Buildings.AsNoTracking().Where(x=>x.IsActive).OrderBy(x=>x.Name).Select(x=>new BuildingDto(x.Id,x.Name,x.Code,x.Rooms.Where(r=>r.IsActive).OrderBy(r=>r.RoomNumber).Select(r=>new RoomDto(r.Id,r.RoomNumber,r.Floor)).ToList())).ToListAsync(ct);
    private async Task ValidateLocation(Guid buildingId,Guid? roomId,CancellationToken ct){if(!await db.Buildings.AnyAsync(x=>x.Id==buildingId&&x.IsActive,ct))throw new ArgumentException("Das ausgewählte Gebäude ist ungültig.");if(roomId.HasValue&&!await db.Rooms.AnyAsync(x=>x.Id==roomId&&x.BuildingId==buildingId&&x.IsActive,ct))throw new ArgumentException("Der Raum gehört nicht zum ausgewählten Gebäude.");}
    private static void Validate(string title,string description){if(string.IsNullOrWhiteSpace(title)||title.Trim().Length is <5 or >150)throw new ArgumentException("Der Titel muss zwischen 5 und 150 Zeichen lang sein.");if(string.IsNullOrWhiteSpace(description)||description.Trim().Length is <10 or >4000)throw new ArgumentException("Die Beschreibung muss zwischen 10 und 4000 Zeichen lang sein.");}
    private static DateTime CalculateDueDate(Priority p)=>DateTime.UtcNow.Add(p switch{Priority.Kritisch=>TimeSpan.FromHours(4),Priority.Hoch=>TimeSpan.FromDays(1),Priority.Mittel=>TimeSpan.FromDays(3),_=>TimeSpan.FromDays(5)});
    private static ServiceRequestDto Map(ServiceRequest x)=>new(x.Id,x.TicketNumber,x.Title,x.Description,x.Category,x.Priority,x.Status,x.BuildingId,x.Building?.Name??"–",x.RoomId,x.Room?.RoomNumber,x.AssignedTechnician,x.DueDateUtc,x.CreatedAtUtc,x.DueDateUtc<DateTime.UtcNow&&x.Status!=ServiceRequestStatus.Geloest&&x.Status!=ServiceRequestStatus.Geschlossen,x.RowVersion);
}
