using LibraryWebApi.Models;
using LibraryWebApi.Services;
using LibraryWebApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace LibraryWebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin")]
public class AdminController : ControllerBase
{
    private readonly IUserService _userService;

    public AdminController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("users")]
    public IActionResult GetAllUsers()
    {
        var users = _userService.GetAllUsersAsync();
        return Ok(users.Result);
    }

    [HttpGet("users/{id}")]
    public IActionResult GetUserById(int id)
    {
        var user = _userService.GetUserByIdAsync(id);

        if (user == null)
        {
            return NotFound();
        }

        return Ok(user.Result);
    }

    [HttpPost("users")]
    public IActionResult CreateUser([FromBody] CreateUserRequest model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            // Создание нового пользователя
            var user = new User
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                DateOfBirth = model.DateOfBirth,
                PhoneNumber = model.PhoneNumber,
                Email = model.Email,
                PersonalCode = model.PersonalCode,
                Password = model.Password,
                Role = model.Role,
                FavoriteBooks = new List<FavoriteBook>()
            };

            _userService.CreateUserAsync(user);

            return Ok(user);
        }
        catch (ApplicationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPatch("users/{id}")]
    public IActionResult UpdateUser(int id, [FromBody] JsonPatchDocument<User> model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
           
             

            var res= _userService.UpdateUserAsync(id, model).Result;
            if (res == null)
            {
                return NotFound();
            }

            return Ok(res);
        }
        catch (ApplicationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpDelete("users/{id}")]
    public IActionResult DeleteUser(int id)
    {
        var result = _userService.DeleteUserAsync(id).Result;
        if (!result)
        {
            return NotFound();
        }
        return Ok();
    } 

    [HttpPost("filter")]
    public async Task<ActionResult<IEnumerable<User>>> FilterUsersAsync(User filter)
    {
        var filteredUsers = await _userService.FilterUsersAsync(filter);
        return Ok(filteredUsers);
    }

    [HttpGet("sort")]
    public async Task<ActionResult<IEnumerable<User>>> SortUsersAsync(string sortBy)
    {
        var sortedUsers = await _userService.SortUsersAsync(sortBy);
        return Ok(sortedUsers);
    }
}

