using DAL;

namespace BLL.DTO;

public class TaskDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string AuthorId { get; set; }
    public CollaboratorDto Author { get; set; }
    public string PerformerId { get; set; }
    public CollaboratorDto? Performer { get; set; }
    public Status Status { get; set; }
    public string? Comment { get; set; }
    public int Priority { get; set; }
}