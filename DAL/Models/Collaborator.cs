using Microsoft.AspNetCore.Identity;

namespace DAL.Models;

public class Collaborator : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Patronymic { get; set; }
    public ICollection<Project>? Projects { get; set; }
    public ICollection<Task>? CreatedTasks { get; set; }
    public ICollection<Task>? Tasks { get; set; }
}