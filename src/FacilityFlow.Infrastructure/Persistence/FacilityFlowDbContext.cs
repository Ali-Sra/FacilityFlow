using FacilityFlow.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FacilityFlow.Infrastructure.Persistence;

public sealed class FacilityFlowDbContext(
    DbContextOptions<FacilityFlowDbContext> options)
    : DbContext(options)
{
    public DbSet<ServiceRequest> ServiceRequests => Set<ServiceRequest>();
    public DbSet<Building> Buildings => Set<Building>();
    public DbSet<Room> Rooms => Set<Room>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Building>(b =>
        {
            b.HasKey(x => x.Id);
            b.HasIndex(x => x.Code).IsUnique();
            b.Property(x => x.Name).HasMaxLength(120);
        });

        modelBuilder.Entity<Room>(r =>
        {
            r.HasKey(x => x.Id);

            r.HasIndex(x => new
            {
                x.BuildingId,
                x.RoomNumber
            }).IsUnique();

            r.HasOne(x => x.Building)
                .WithMany(x => x.Rooms)
                .HasForeignKey(x => x.BuildingId);
        });

        modelBuilder.Entity<ServiceRequest>(s =>
        {
            s.HasKey(x => x.Id);

            s.HasIndex(x => x.TicketNumber).IsUnique();
            s.HasIndex(x => x.Status);
            s.HasIndex(x => x.Priority);
            s.HasIndex(x => x.DueDateUtc);

            s.Property(x => x.Title)
                .HasMaxLength(150);

            s.Property(x => x.Description)
                .HasMaxLength(4000);

            s.Property(x => x.RowVersion)
                .IsRequired()
                .IsConcurrencyToken()
                .ValueGeneratedNever();

            s.HasOne(x => x.Building)
                .WithMany()
                .HasForeignKey(x => x.BuildingId)
                .OnDelete(DeleteBehavior.Restrict);

            s.HasOne(x => x.Room)
                .WithMany()
                .HasForeignKey(x => x.RoomId)
                .OnDelete(DeleteBehavior.Restrict);

            s.HasQueryFilter(x => !x.IsDeleted);
        });
    }
}