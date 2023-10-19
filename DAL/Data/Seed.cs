using DAL.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Task = System.Threading.Tasks.Task;

namespace DAL.Data;

public class Seed
{
    private readonly DataContext _dataContext;

    public Seed(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public void SeedData()
    {
        if (_dataContext.Projects.Any()) return;

        var projects = new List<Project>
        {
            new()
            {
                Priority = (int) ProjectPriority.Medium,
                Name = "New car business",
                CustomerCompanyName = "NGA International Company",
                PerformerCompanyName = "NB Group",
                ProjectStartDate = new DateOnly(2017, 5, 23),
                ProjectEndDate = new DateOnly(2019, 10, 12),
            }
        };
            
        _dataContext.Projects.AddRange(projects);
        _dataContext.SaveChanges();
    }

    public async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
    {
        using var serviceScope = applicationBuilder.ApplicationServices.CreateScope();
        var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        
        if (await roleManager.RoleExistsAsync(UserRoles.Director) == false)
            await roleManager.CreateAsync(new IdentityRole(UserRoles.Director));
        if (await roleManager.RoleExistsAsync(UserRoles.ProjectManager) == false)
            await roleManager.CreateAsync(new IdentityRole(UserRoles.ProjectManager));
        if (await roleManager.RoleExistsAsync(UserRoles.Collaborator) == false)
            await roleManager.CreateAsync(new IdentityRole(UserRoles.Collaborator));
        
        var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<Collaborator>>();
        const string directorUserEmail = "director@gmail.com";
        var directorUser = await userManager.FindByEmailAsync(directorUserEmail);
        
        if (directorUser == null)
        {
            var newDirectorUser = new Collaborator
            {
                FirstName = "Jhon",
                LastName = "Stuart",
                Patronymic = "Johansson",
                UserName = "director",
                Email = directorUserEmail,
                EmailConfirmed = true
            };
            await userManager.CreateAsync(newDirectorUser, "Director1234@");
            await userManager.AddToRoleAsync(newDirectorUser, UserRoles.Director);
        }
        
        const string managerUserEmail = "manager@gmail.com";
        var managerUser = await userManager.FindByEmailAsync(managerUserEmail);
        
        if (managerUser == null)
        {
            var newManagerUser = new Collaborator
            {
                FirstName = "Sam",
                LastName = "Smith",
                Patronymic = "Richardson",
                UserName = "project-manager",
                Email = managerUserEmail,
                EmailConfirmed = true
            };
            await userManager.CreateAsync(newManagerUser, "Manager1234@");
            await userManager.AddToRoleAsync(newManagerUser, UserRoles.ProjectManager);
        }
            
        const string collaboratorUserEmail = "collaborator@gmail.com";
        var collaboratorUser = await userManager.FindByEmailAsync(collaboratorUserEmail);
        
        if (collaboratorUser == null)
        {
            var newCollaboratorUser = new Collaborator
            {
                FirstName = "Jack",
                LastName = "Gustav",
                Patronymic = "Josefsson",
                UserName = "collaborator",
                Email = collaboratorUserEmail,
                EmailConfirmed = true
            };
            await userManager.CreateAsync(newCollaboratorUser, "Collaborator1234@");
            await userManager.AddToRoleAsync(newCollaboratorUser, UserRoles.Collaborator);
        }
    }
}