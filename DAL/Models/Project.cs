namespace DAL.Models;

public class Project
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string CustomerCompanyName { get; set; }
    public string PerformerCompanyName { get; set; }
    public string? DirectorId { get; set; }
    public ICollection<Collaborator>? Collaborators { get; set; }
    public ICollection<Task>? Tasks { get; set; }
    public int Priority { get; set; }
    public DateOnly ProjectStartDate { get; set; }
    public DateOnly ProjectEndDate { get; set; }
    public ICollection<string>? FilesUrl { get; set; }
}