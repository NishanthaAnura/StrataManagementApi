namespace WebApi.Models;
public class MaintenanceRequestResponse
{
    public string Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public MaintenanceStatus Status { get; set; }
    public DateTime LastChangedTime { get; set; }
    public string UserName { get; set; }
    public string BuildingName { get; set; }
    public string UnitNumber { get; set; }

}
