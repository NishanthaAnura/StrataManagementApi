namespace WebApi.Models;
public class TenantResponse
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Contact { get; set; }
    public string BuildingId { get; set; }
    public string AssignedUnit { get; set; }
    public BuildingResponse Building { get; set; }
    public ICollection<MaintenanceRequestResponse> MaintenanceRequests { get; set; }

}
