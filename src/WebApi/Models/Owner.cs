using System.ComponentModel.DataAnnotations;
using WebApi.Utilities;

namespace WebApi.Models;
public class Owner
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
    public string AssignedBuildingId { get; set; }

    // Navigation property
    public Building AssignedBuilding { get; set; }
    public ICollection<MaintenanceRequest> MaintenanceRequests { get; set; } = new List<MaintenanceRequest>();

}
