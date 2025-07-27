using BudgetBeavers.Application.Dtos.UserDtos;
using BudgetBeavers.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BudgetBeavers.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController(IUserService usersService) : ControllerBase
{
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetUserById(Guid id)
    {
        var user = await usersService.GetByIdAsync(id);
        return Ok(user);
    }
    
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteUserAsync(Guid id)
    {
        await usersService.DeleteAsync(id);
        return NoContent();
    }
    
    [HttpPost]
    public async Task<IActionResult> AddUserAsync([FromBody] CreateUserDto createUserDto)
    {
        var createdUser = await usersService.AddAsync(createUserDto);
        return CreatedAtAction(nameof(GetUserById), new { id = createdUser.Id }, createdUser);
    }
    
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateUserAsync(Guid id, [FromBody] UpdateUserDto updateUserDto)
    {
        var updatedUser = await usersService.UpdateAsync(id, updateUserDto);
        return Ok(updatedUser);
    }
}