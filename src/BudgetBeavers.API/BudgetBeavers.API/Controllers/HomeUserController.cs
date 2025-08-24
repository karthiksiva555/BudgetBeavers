using BudgetBeavers.Application.Dtos.HomeUserDtos;
using BudgetBeavers.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BudgetBeavers.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HomeUserController(IHomeUserService homeUserService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> AddMemberToHome([FromBody] CreateHomeUserDto createHomeUserDto)
    {
        var createdHomeUser = await homeUserService.AddAsync(createHomeUserDto);
        return CreatedAtAction(nameof(GetMembersByHomeId), new { homeId = createdHomeUser.HomeId }, createdHomeUser);
    }
    
    [HttpPut]
    public async Task<IActionResult> UpdateHomeUser(Guid id, [FromBody] UpdateHomeUserDto updateHomeUserDto)
    {
        var updatedHomeUser = await homeUserService.UpdateAsync(id, updateHomeUserDto);
        return Ok(updatedHomeUser);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> RemoveMemberFromHome(Guid id)
    {
        await homeUserService.DeleteAsync(id);
        return NoContent();
    }

    [HttpGet]
    public async Task<IActionResult> GetHomeUserById(Guid homeUserId)
    {
        var homeUser = await homeUserService.GetByIdAsync(homeUserId);
        return Ok(homeUser);
    }
    
    [HttpGet("members/{homeId:guid}")]
    public async Task<IActionResult> GetMembersByHomeId(Guid homeId)
    {
        var users = await homeUserService.GetMembersByHomeIdAsync(homeId);
        return Ok(users);
    }
}