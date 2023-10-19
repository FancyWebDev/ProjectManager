using Microsoft.AspNetCore.Http;

namespace BLL.DTO;

public class ProjectDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string CustomerCompanyName { get; set; }
    public string PerformerCompanyName { get; set; }
    public string? DirectorId { get; set; }
    public IEnumerable<string>? CollaboratorsId { get; set; }
    public IEnumerable<CollaboratorDto>? Collaborators { get; set; }
    public IEnumerable<TaskDto>? Tasks { get; set; }
    public int Priority { get; set; }
    public string ProjectStartDate { get; set; }
    public string ProjectEndDate { get; set; }
    public IEnumerable<IFormFile>? Files { get; set; }
}