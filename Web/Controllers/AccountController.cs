using System.Net;
using BLL;
using BLL.DTO;
using BLL.Services;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase
{
    private readonly AccountService _accountService;

    public AccountController(AccountService accountService)
    {
        _accountService = accountService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
    {
        if (ModelState.IsValid == false || registerDto == null)
            return Problem("Invalid email or password", statusCode: (int?) HttpStatusCode.BadRequest);

        var user = await _accountService.FindByEmailAsync(registerDto.Email);
        if (user != null)
            return Problem("This email address is already in use", statusCode: (int?) HttpStatusCode.BadRequest);

        var isSucceeded = await _accountService.CreateAsync(registerDto, UserRoleType.Collaborator);
        if (isSucceeded == false)
            return Problem("Your password must be at least 8 characters long, contain at least one number, non alphanumeric characters ~!@#$%^&*()_+-[]{}|':,./<>? and have a mixture of uppercase and lowercase letters",
                statusCode: (int?) HttpStatusCode.BadRequest);
            
        return NoContent();
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        if (ModelState.IsValid == false || loginDto == null)
            return Problem("Email or password is incorrect", statusCode: (int?) HttpStatusCode.BadRequest);

        var user = await _accountService.FindByEmailAsync(loginDto.Email);
        if (user == null)
            return Problem("User not found", statusCode: (int?) HttpStatusCode.BadRequest);

        var isPasswordCorrect = await _accountService.CheckPasswordAsync(user, loginDto.Password);
        if (isPasswordCorrect == false)
            return Problem("Wrong credentials", statusCode: (int?) HttpStatusCode.BadRequest);

        var result = await _accountService.PasswordSignInAsync(user, loginDto.Password);
        return result.Succeeded ? NoContent() : Problem("Wrong credentials", statusCode: (int?) HttpStatusCode.BadRequest);
    }

    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        await _accountService.Logout();
        return NoContent();
    }
}