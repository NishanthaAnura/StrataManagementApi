using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text.Json;
using WebApi.Models;
using WebApi.Services;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BuildingController(IBuildingService buildingService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await buildingService.GetAllAsync();
        if (!result.IsSuccess)
        {
            return BadRequest(result.ErrorMessage);
        }
        return Ok(result.Value);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        var result = await buildingService.GetByIdAsync(id);
        if (!result.IsSuccess)
        {
            return NotFound(result.ErrorMessage);
        }
        return Ok(result.Value);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] BuildingRequest request)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var result = await buildingService.CreateAsync(request);
        if (!result.IsSuccess)
        {
            return BadRequest(result.ErrorMessage);
        }
        return Ok(result.Value);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, BuildingRequest request)
    {
        var result = await buildingService.UpdateAsync(id, request);
        if (!result.IsSuccess)
        {
            return BadRequest(result.ErrorMessage);
        }
        return Ok(result.Value);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var result = await buildingService.DeleteAsync(id);
        if (!result.IsSuccess)
        {
            return BadRequest(result.ErrorMessage);
        }
        return NoContent();
    }

}
