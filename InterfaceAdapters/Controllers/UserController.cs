using Application.DTO;
using Application.IService;
using Microsoft.AspNetCore.Mvc;
namespace InterfaceAdapters.Controllers;

[Route("api/users")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    List<string> _errorMessages = new List<string>();
    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    // Get: api/users
    [HttpGet]
    public async Task<ActionResult<UserDTO>> GetUsers()
    {
        var result = await _userService.GetAll();
        return Ok(result);
    }
}
