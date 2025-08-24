using BudgetBeavers.Application.Dtos.HomeDtos;
using BudgetBeavers.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BudgetBeavers.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HomesController(IHomeService homeService) : ControllerBase
{
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetHomeById(Guid id)
    {
        var home = await homeService.GetByIdAsync(id);
        return Ok(home);
    }
    
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteHome(Guid id)
    {
        await homeService.DeleteAsync(id);
        return NoContent();
    }
    
    [HttpPost]
    public async Task<IActionResult> AddHome([FromBody] CreateHomeDto createHomeDto)
    {
        var createdHome = await homeService.AddAsync(createHomeDto);
        return CreatedAtAction(nameof(GetHomeById), new { id = createdHome.Id }, createdHome);
    }
    
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateHome(Guid id, [FromBody] UpdateHomeDto updateHomeDto)
    {
        var updatedHome = await homeService.UpdateAsync(id, updateHomeDto);
        return Ok(updatedHome);
    }
}