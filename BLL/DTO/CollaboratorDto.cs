namespace BLL.DTO;

public class CollaboratorDto
{
    public string Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Patronymic { get; set; }
    public string Email { get; set; }
    public IEnumerable<ProjectDto>? Projects { get; set; }
    public IEnumerable<TaskDto>? CreatedTasks { get; set; }
    public IEnumerable<TaskDto>? Tasks { get; set; }
}