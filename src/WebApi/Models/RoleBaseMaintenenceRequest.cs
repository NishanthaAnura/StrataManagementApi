using System.ComponentModel.DataAnnotations;

namespace WebApi.Models;
public class RoleBaseMaintenenceRequest
{
    [Required]
    public string Role { get; set; }
    public string? BuildingId { get; set; }
    public string? UnitNumber { get; set; }
}
