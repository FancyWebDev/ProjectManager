using BLL.DTO;
using DAL.Models;
using Task = System.Threading.Tasks.Task;

namespace BLL.Services;

public interface IProjectService
{
    IEnumerable<ProjectDto> GetAll();
    ProjectDto Get(Func<Project, bool> predicate);
    bool Exist(Func<Project, bool> predicate);
    Task Add(ProjectDto projectDto);
    void Update(ProjectDto projectDto);
    void Delete(ProjectDto projectDto);
}