namespace WebApi.Models;
public class OwnerResponse
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Contact { get; set; }
    public string AssignedBuildingId { get; set; }
    public BuildingResponse AssignedBuilding { get; set; }
    public ICollection<MaintenanceRequestResponse> MaintenanceRequests { get; set; }

}
