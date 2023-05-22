using LibraryWebApi.Models;
using LibraryWebApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace LibraryWebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IPasswordHasher _passwordHasher;
    private readonly ITokenService _tokenService;

    public AccountController(
        IUserService userService,
        IPasswordHasher passwordHasher,
        ITokenService tokenService)
    {
        _userService = userService;
        _passwordHasher = passwordHasher;
        _tokenService = tokenService;
    }

    [HttpPost("register")]
    public IActionResult Register([FromBody] CreateUserRequest model)
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
                FinancialCode = model.FinancialCode,
                Password = model.Password,
                Role = "User" 
            };

            _userService.Create(user, user.Password);

            var token = _tokenService.GenerateToken(user);

            return Ok(new { Token = token });
        }
        catch (ApplicationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var user = _userService.Authenticate(model.Email, model.Password);

        if (user == null)
        {
            return Unauthorized();
        }

        var token = _tokenService.GenerateToken(user); 

        // Установка токена в заголовке ответа
        Response.Headers.Add("Authorization", $"Bearer {token}");

        return Ok(new { Token = token });
    }
}
