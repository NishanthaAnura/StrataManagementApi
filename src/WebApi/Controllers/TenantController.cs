using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebApi.Models;
using WebApi.Services;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TenantController(ITenantService tenantService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await tenantService.GetAllAsync();
        if (!result.IsSuccess)
        {
            return BadRequest(result.ErrorMessage);
        }
        return Ok(result.Value);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        var result = await tenantService.GetByIdAsync(id);
        if (!result.IsSuccess)
        {
            return NotFound(result.ErrorMessage);
        }
        return Ok(result.Value);
    }

    [HttpGet("by-email/{email}")]
    public async Task<IActionResult> GetByEmail(string email)
    {
        var result = await tenantService.GetByEmailAsync(email);
        if (!result.IsSuccess)
        {
            return NotFound(result.ErrorMessage);
        }
        return Ok(result.Value);
    }

    [HttpPost]
    public async Task<IActionResult> Create(TenantRequest request)
    {
        var result = await tenantService.CreateAsync(request);
        if (!result.IsSuccess)
        {
            return BadRequest(result.ErrorMessage);
        }
        return Ok(result.Value);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, TenantRequest request)
    {
        var result = await tenantService.UpdateAsync(id, request);
        if (!result.IsSuccess)
        {
            return BadRequest(result.ErrorMessage);
        }
        return Ok(result.Value);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var result = await tenantService.DeleteAsync(id);
        if (!result.IsSuccess)
        {
            return BadRequest(result.ErrorMessage);
        }
        return NoContent();
    }

}
