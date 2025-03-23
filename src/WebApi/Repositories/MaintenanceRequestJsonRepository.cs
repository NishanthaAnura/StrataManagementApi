using System.Text.Json;
using WebApi.Models;
using WebApi.Utilities;

namespace WebApi.Repositories;
public class MaintenanceRequestJsonRepository

{
    private const string JsonFilePath = "Data/maintenanceRequests.json";

    public Task<Result<IEnumerable<MaintenanceRequest>>> GetAll()
    {
        if (!File.Exists(JsonFilePath))
        {
            return Task.FromResult(Result<IEnumerable<MaintenanceRequest>>.Failure("JSON file not found."));
        }

        var jsonData = File.ReadAllText(JsonFilePath);
        var jsonObject = JsonSerializer.Deserialize<JsonElement>(jsonData);

        // Check if the JSON has a root object with a "MaintenanceRequests" property
        if (jsonObject.ValueKind == JsonValueKind.Object && jsonObject.TryGetProperty("MaintenanceRequests", out var requestsArray))
        {
            var requests = JsonSerializer.Deserialize<List<MaintenanceRequest>>(requestsArray.ToString());
            return Task.FromResult(Result<IEnumerable<MaintenanceRequest>>.Success(requests));
        }

        return Task.FromResult(Result<IEnumerable<MaintenanceRequest>>.Failure("Invalid JSON structure."));
    }


    public Task<Result<MaintenanceRequest>> GetById(string id)
    {
        var requests = GetAll().Result.Value;
        var request = requests.FirstOrDefault(r => r.Id == id);
        if (request == null)
        {
            return Task.FromResult(Result<MaintenanceRequest>.Failure($"Request with Id {id} not found."));
        }
        return Task.FromResult(Result<MaintenanceRequest>.Success(request));
    }


    public Task<Result<MaintenanceRequest>> Create(MaintenanceRequest request)
    {
        var requests = GetAll().Result.Value.ToList();
        request.Id = MbcmId.NewId();
        request.Status = MaintenanceStatus.Pending; // Default status
        requests.Add(request);
        SaveData(requests);

        return Task.FromResult(Result<MaintenanceRequest>.Success(request));
    }

    public Task<Result<MaintenanceRequest>> Update(string id, MaintenanceRequest request)
    {
        var requests = GetAll().Result.Value.ToList();
        var existing = requests.FirstOrDefault(r => r.Id == id);
        if (existing == null)
        {
            return Task.FromResult(Result<MaintenanceRequest>.Failure($"Request with Id {id} not found."));
        }

        existing.Title = request.Title;
        existing.Description = request.Description;
        existing.Status = request.Status;
        existing.LastChangedTime = DateTime.UtcNow;
        SaveData(requests);

        return Task.FromResult(Result<MaintenanceRequest>.Success(existing));
    }

    public Task<Result<MaintenanceRequest>> Delete(string id)
    {
        var requests = GetAll().Result.Value.ToList();
        var request = requests.FirstOrDefault(r => r.Id == id);
        if (request == null)
        {
            return Task.FromResult(Result<MaintenanceRequest>.Failure($"Request with ID {id} not found."));
        }

        requests.Remove(request);
        SaveData(requests);

        return Task.FromResult(Result<MaintenanceRequest>.Success(request));
    }

    private void SaveData(List<MaintenanceRequest> requests)
    {
        var json = JsonSerializer.Serialize(new { MaintenanceRequests = requests }, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(JsonFilePath, json);
    }



}
