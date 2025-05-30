using Microsoft.AspNetCore.Mvc;

namespace BudgetBeavers.API.Controllers;

[ApiController]
[Route("[controller]")]
public class HomeController : ControllerBase
{
    [HttpGet("{id:guid}")]
    public IActionResult GetHomeById(Guid id)
    {
        
        return Ok();
    }
}