using FacilityFlow.Domain.Common;
namespace FacilityFlow.Domain.Entities;
public sealed class Building : BaseEntity
{
    public required string Name { get; set; }
    public required string Code { get; set; }
    public required string Address { get; set; }
    public required string City { get; set; }
    public required string PostalCode { get; set; }
    public bool IsActive { get; set; } = true;
    public ICollection<Room> Rooms { get; set; } = new List<Room>();
}
