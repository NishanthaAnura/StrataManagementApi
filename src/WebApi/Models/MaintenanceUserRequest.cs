using System.ComponentModel.DataAnnotations;

namespace WebApi.Models;
public class MaintenanceUserRequest
{
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
}
