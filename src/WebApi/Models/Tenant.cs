using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using WebApi.Utilities;

namespace WebApi.Models;
public class Tenant
{
    [Key]
    public string Id { get; set; } = MbcmId.NewId();

    [Required]
    [MaxLength(100)]
    public string Name { get; set; }

    [Required]
    [MaxLength(100)]
    public string Contact { get; set; }

    [Required]
    public string BuildingId { get; set; }

    [Required]
    public string AssignedUnit { get; set; }

    // Navigation property
    public Building Building { get; set; }
    public ICollection<MaintenanceRequest> MaintenanceRequests { get; set; } = new List<MaintenanceRequest>();

}
