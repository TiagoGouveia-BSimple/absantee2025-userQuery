using Application.DTO;
using Application.Services;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
namespace InterfaceAdapters.Controllers;

[Route("api/users")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly UserService _userService;
    List<string> _errorMessages = new List<string>();
    public UserController(UserService userService)
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
