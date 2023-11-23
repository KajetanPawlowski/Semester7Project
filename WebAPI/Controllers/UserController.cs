using Application.LogicInterface;
using Domain.DTO;
using Domain.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;
[ApiController]
[Route("[controller]")]
[Authorize]
public class UserController: ControllerBase
{
    private readonly IConfiguration config;
    private readonly IUserLogic _userLogic;

    public UserController(IConfiguration config, IUserLogic userLogic)
    {
        this.config = config;
        _userLogic = userLogic;
    } 
    //Get USERS !!! not secure
    [HttpGet,  AllowAnonymous]
    public async Task<ActionResult<List<User>>> GetSuppliersAsync()
    {
        try
        {
            List<User> users = await _userLogic.GetUsersAsync();
            return Ok(users);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    //Post user
    [HttpPost,  AllowAnonymous]
    public async Task<ActionResult<User>> CreateAsync(RegisterUserDTO dto)
    {
        try
        {
            User user = await _userLogic.RegisterUserAsync(dto);
            return Created($"/User/{user.Id}", user);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
}