using System.ComponentModel.DataAnnotations;

namespace WebApi.Models;
public class BuildingRequest
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; }

    [Required]
    [MaxLength(200)]
    public string Address { get; set; }

    public int NumberOfUnits { get; set; }

}
