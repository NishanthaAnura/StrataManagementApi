using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using WebApi.Utilities;

namespace WebApi.Models;
public class Building
{
    [Key]
    public string Id { get; set; } = MbcmId.NewId();

    [Required]
    [MaxLength(100)]
    public string Name { get; set; }

    [Required]
    [MaxLength(200)]
    public string Address { get; set; }

    public int NumberOfUnits { get; set; }

    // Navigation properties
    public ICollection<Owner> Owners { get; set; } = new List<Owner>();
    public ICollection<Tenant> Tenants { get; set; } = new List<Tenant>();
    public ICollection<MaintenanceRequest> MaintenanceRequests { get; set; } = new List<MaintenanceRequest>();

}
