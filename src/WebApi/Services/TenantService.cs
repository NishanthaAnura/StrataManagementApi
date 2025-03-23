using WebApi.Models;
using WebApi.Repositories;
using WebApi.Utilities;

namespace WebApi.Services;
public class TenantService(ITenantRepositery tenantRepository) : ITenantService
{
    public async Task<Result<IEnumerable<TenantResponse>>> GetAllAsync()
    {
        var result = await tenantRepository.GetAll();
        if (!result.IsSuccess || result.Value == null)
        {
            return Result<IEnumerable<TenantResponse>>.Failure(result.ErrorMessage ?? "Failed to retrieve tenants.");
        }

        var tenantResponses = result.Value.Select(tenant => MapToTenantResponse(tenant)).ToList();
        return Result<IEnumerable<TenantResponse>>.Success(tenantResponses);
    }

    public async Task<Result<TenantResponse>> GetByIdAsync(string id)
    {
        var result = await tenantRepository.GetById(id);
        if (!result.IsSuccess || result.Value == null)
        {
            return Result<TenantResponse>.Failure(result.ErrorMessage ?? "Tenant not found.");
        }

        var tenantResponse = MapToTenantResponse(result.Value);
        return Result<TenantResponse>.Success(tenantResponse);
    }

    public async Task<Result<TenantResponse>> CreateAsync(TenantRequest request)
    {
        var newTenant = new Tenant
        {
            Name = request.Name,
            Contact = request.Contact,
            BuildingId = request.BuildingId,
            AssignedUnit = request.AssignedUnit
        };

        var result = await tenantRepository.Create(newTenant);
        if (!result.IsSuccess || result.Value == null)
        {
            return Result<TenantResponse>.Failure(result.ErrorMessage ?? "Failed to create tenant.");
        }

        var tenantResponse = MapToTenantResponse(result.Value);
        return Result<TenantResponse>.Success(tenantResponse);
    }

    public async Task<Result<TenantResponse>> UpdateAsync(string id, TenantRequest request)
    {
        var existingTenantResult = await tenantRepository.GetById(id);
        if (!existingTenantResult.IsSuccess || existingTenantResult.Value == null)
        {
            return Result<TenantResponse>.Failure("Tenant not found.");
        }

        var existingTenant = existingTenantResult.Value;
        existingTenant.Name = request.Name;
        existingTenant.Contact = request.Contact;
        existingTenant.BuildingId = request.BuildingId;
        existingTenant.AssignedUnit = request.AssignedUnit;

        var updateResult = await tenantRepository.Update(id, existingTenant);
        if (!updateResult.IsSuccess || updateResult.Value == null)
        {
            return Result<TenantResponse>.Failure(updateResult.ErrorMessage ?? "Failed to update tenant.");
        }

        var tenantResponse = MapToTenantResponse(updateResult.Value);
        return Result<TenantResponse>.Success(tenantResponse);
    }

    public async Task<Result<bool>> DeleteAsync(string id)
    {
        var result = await tenantRepository.Delete(id);
        if (!result.IsSuccess || result.Value == null)
        {
            return Result<bool>.Failure(result.ErrorMessage ?? "Failed to delete tenant.");
        }

        return Result<bool>.Success(true);
    }

    public async Task<Result<TenantResponse>> GetByEmailAsync(string email)
    {
        var result = await tenantRepository.GetByEmail(email);
        if (!result.IsSuccess || result.Value == null)
        {
            return Result<TenantResponse>.Failure(result.ErrorMessage ?? "Tenant not found.");
        }

        var ownerResponse = MapToTenantResponse(result.Value);
        return Result<TenantResponse>.Success(ownerResponse);
    }

    private TenantResponse MapToTenantResponse(Tenant tenant)
    {
        return new TenantResponse
        {
            Id = tenant.Id,
            Name = tenant.Name,
            Contact = tenant.Contact,
            BuildingId = tenant.BuildingId,
            AssignedUnit = tenant.AssignedUnit,
            Building = new BuildingResponse
            {
                Id = tenant.Building.Id,
                Name = tenant.Building.Name,
                Address = tenant.Building.Address,
                NumberOfUnits = tenant.Building.NumberOfUnits
            },
            MaintenanceRequests = tenant.MaintenanceRequests
                .Where(mr => mr.UnitNumber != null && mr.UnitNumber == tenant.AssignedUnit)
                .Select(mr => new MaintenanceRequestResponse
                {
                    Id = mr.Id,
                    Title = mr.Title,
                    Description = mr.Description,
                    Status = mr.Status,
                    LastChangedTime = mr.LastChangedTime,
                    BuildingName = mr.Building?.Name ?? "Unknown"
                })
                .ToList()
        };
    }

}
