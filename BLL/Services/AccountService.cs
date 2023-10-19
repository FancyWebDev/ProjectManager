using BLL.DTO;
using DAL.Data;
using DAL.Models;
using Microsoft.AspNetCore.Identity;
using Task = System.Threading.Tasks.Task;

namespace BLL.Services;

public class AccountService
{
    private readonly UserManager<Collaborator> _userManager;
    private readonly SignInManager<Collaborator> _signInManager;

    public AccountService(UserManager<Collaborator> userManager, SignInManager<Collaborator> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task<Collaborator?> FindByEmailAsync(string email) => 
        await _userManager.FindByEmailAsync(email);

    public async Task<bool> CheckPasswordAsync(Collaborator user, string password) =>
        await _userManager.CheckPasswordAsync(user, password);

    public async Task<SignInResult> PasswordSignInAsync(Collaborator user, string password) => 
        await _signInManager.PasswordSignInAsync(user, password, false, false);

    public async Task<bool> CreateAsync(RegisterDto registerDto, UserRoleType role)
    {
        var newUser = new Collaborator
        {
            UserName = registerDto.Email,
            FirstName = registerDto.FirstName,
            LastName = registerDto.LastName,
            Patronymic = registerDto.Patronymic,
            Email = registerDto.Email,
            EmailConfirmed = true
        };
            
        var result = await _userManager.CreateAsync(newUser, registerDto.Password);

        if (result.Succeeded) 
            await AddToRoleAsync(newUser, role);

        return result.Succeeded;
    }

    private async Task AddToRoleAsync(Collaborator user, UserRoleType role)
    {
        switch (role)
        {
            case UserRoleType.Collaborator:
                await _userManager.AddToRoleAsync(user, UserRoles.Collaborator);
                break;
            case UserRoleType.ProjectManager:
                await _userManager.AddToRoleAsync(user, UserRoles.ProjectManager);
                break;
            case UserRoleType.Director:
                await _userManager.AddToRoleAsync(user, UserRoles.Director);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(role), role, null);
        }
    }

    public async Task Logout() => await _signInManager.SignOutAsync();
}