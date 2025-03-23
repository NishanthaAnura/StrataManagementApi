using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Services;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OwnerController(IOwnerService ownerService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await ownerService.GetAllAsync();
        if (!result.IsSuccess)
        {
            return BadRequest(result.ErrorMessage);
        }
        return Ok(result.Value);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        var result = await ownerService.GetByIdAsync(id);
        if (!result.IsSuccess)
        {
            return NotFound(result.ErrorMessage);
        }
        return Ok(result.Value);
    }

    [HttpGet("by-email/{email}")]
    public async Task<IActionResult> GetByEmail(string email)
    {
        var result = await ownerService.GetByEmailAsync(email);
        if (!result.IsSuccess)
        {
            return NotFound(result.ErrorMessage);
        }
        return Ok(result.Value);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] OwnerRequest request)
    {

        var result = await ownerService.CreateAsync(request);
        if (!result.IsSuccess)
        {
            return BadRequest(result.ErrorMessage);
        }
        return Ok(result.Value);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, OwnerRequest request)
    {
        var result = await ownerService.UpdateAsync(id, request);
        if (!result.IsSuccess)
        {
            return BadRequest(result.ErrorMessage);
        }
        return Ok(result.Value);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var result = await ownerService.DeleteAsync(id);
        if (!result.IsSuccess)
        {
            return BadRequest(result.ErrorMessage);
        }
        return NoContent();
    }
}
