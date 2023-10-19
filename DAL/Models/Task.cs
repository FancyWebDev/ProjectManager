namespace DAL.Models;

public class Task
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string AuthorId { get; set; }
    public Collaborator Author { get; set; }
    public string? PerformerId { get; set; }
    public Collaborator? Performer { get; set; }
    public Status Status { get; set; }
    public string? Comment { get; set; }
    public int Priority { get; set; }
}