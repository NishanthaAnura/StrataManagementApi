using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WebApi.Models;
public class OwnerRequest
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; }

    [Required]
    [MaxLength(100)]
    public string Contact { get; set; }

    [Required]
    public string AssignedBuildingId { get; set; }
}
