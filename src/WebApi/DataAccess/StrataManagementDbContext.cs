using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApi.Models;

namespace WebApi.DataAccess;
public class StrataManagementDbContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
{
    public StrataManagementDbContext(DbContextOptions<StrataManagementDbContext> options) : base(options) { }

    public DbSet<Building> Buildings { get; set; }
    public DbSet<Owner> Owners { get; set; }
    public DbSet<Tenant> Tenants { get; set; }
    public DbSet<MaintenanceRequest> MaintenanceRequests { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure Building entity
        modelBuilder.Entity<Building>()
            .HasKey(b => b.Id);

        modelBuilder.Entity<Building>()
            .Property(b => b.Name)
            .HasMaxLength(100)
            .IsRequired();

        modelBuilder.Entity<Building>()
            .Property(b => b.Address)
            .HasMaxLength(200)
            .IsRequired();

        // Configure Owner entity
        modelBuilder.Entity<Owner>()
            .HasKey(o => o.Id);

        modelBuilder.Entity<Owner>()
            .HasOne(o => o.AssignedBuilding)
            .WithMany(b => b.Owners)
            .HasForeignKey(o => o.AssignedBuildingId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Owner>()
            .Property(t => t.AssignedBuildingId)
            .IsRequired();

        // Configure Tenant entity
        modelBuilder.Entity<Tenant>()
            .HasKey(t => t.Id);

        modelBuilder.Entity<Tenant>()
            .HasOne(t => t.Building)
            .WithMany(b => b.Tenants)
            .HasForeignKey(t => t.BuildingId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Tenant>()
            .Property(t => t.AssignedUnit)
            .IsRequired();

        // Configure MaintenanceRequest entity
        modelBuilder.Entity<MaintenanceRequest>()
            .HasKey(mr => mr.Id);

        modelBuilder.Entity<MaintenanceRequest>()
            .HasOne(mr => mr.Building)
            .WithMany(b => b.MaintenanceRequests)
            .HasForeignKey(mr => mr.BuildingId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<MaintenanceRequest>()
            .Property(mr => mr.Title)
            .HasMaxLength(100)
            .IsRequired();

        modelBuilder.Entity<MaintenanceRequest>()
            .Property(mr => mr.Description)
            .HasMaxLength(500)
            .IsRequired();

        modelBuilder.Entity<MaintenanceRequest>()
            .Property(mr => mr.Status)
            .IsRequired();

        modelBuilder.Entity<MaintenanceRequest>()
            .Property(mr => mr.LastChangedTime)
            .IsRequired();
    }
}
