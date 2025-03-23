using System.ComponentModel.DataAnnotations;
using WebApi.Utilities;

namespace WebApi.Models;
public class MaintenanceRequest
{
    [Key]
    public string Id { get; set; } = MbcmId.NewId();

    [Required]
    [MaxLength(100)]
    public string Title { get; set; }

    [Required]
    [MaxLength(500)]
    public string Description { get; set; }

    [Required]
    public MaintenanceStatus Status { get; set; }

    [Required]
    public string BuildingId { get; set; }

    public string? UnitNumber { get; set; }

    public DateTime LastChangedTime { get; set; } = DateTime.UtcNow;

    // Navigation properties
    public Building Building { get; set; }

}
