using FacilityFlow.Domain.Common;
namespace FacilityFlow.Domain.Entities;
public sealed class Room : BaseEntity
{
    public Guid BuildingId { get; set; }
    public required string RoomNumber { get; set; }
    public required string Floor { get; set; }
    public string? Description { get; set; }
    public bool IsActive { get; set; } = true;
    public Building? Building { get; set; }
}
