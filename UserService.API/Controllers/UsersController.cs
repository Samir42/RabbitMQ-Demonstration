using Microsoft.AspNetCore.Mvc;
using UserService.API.Entities;
using UserService.API.Services.Abstraction;

namespace UserService.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateUserAsync([FromBody] User user)
    {
        var createdUser = await _userService.CreateUserAsync(user);

        return Ok(createdUser);
    }
}