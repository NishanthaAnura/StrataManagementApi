using System.ComponentModel.DataAnnotations;

namespace WebApi.Models;
public class MaintenanceUpdatedRequest
{
    public MaintenanceStatus Status { get; set; }
}
