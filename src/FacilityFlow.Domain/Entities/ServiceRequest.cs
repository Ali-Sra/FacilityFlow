using FacilityFlow.Domain.Common;
using FacilityFlow.Domain.Enums;
namespace FacilityFlow.Domain.Entities;
public sealed class ServiceRequest : BaseEntity
{
    public required string TicketNumber { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }
    public ServiceCategory Category { get; set; }
    public Priority Priority { get; set; }
    public ServiceRequestStatus Status { get; set; } = ServiceRequestStatus.Neu;
    public Guid BuildingId { get; set; }
    public Guid? RoomId { get; set; }
    public string? AssignedTechnician { get; set; }
    public DateTime DueDateUtc { get; set; }
    public DateTime? ResolvedAtUtc { get; set; }
    public string? ResolutionDescription { get; set; }
    public bool IsDeleted { get; set; }
    public byte[] RowVersion { get; set; } = Array.Empty<byte>();
    public Building? Building { get; set; }
    public Room? Room { get; set; }
}
