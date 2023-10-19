using DAL.Data;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repository;

public interface IProjectRepository : IRepository<Project> { }
    
public class ProjectRepository : IProjectRepository
{
    private readonly DataContext _dataContext;

    public ProjectRepository(DataContext dataContext)
    {
        _dataContext = dataContext;
    }
        
    public IEnumerable<Project> GetAll()
    {
        return _dataContext.Projects
            .Include(project => project.Collaborators);
    }
        
    public Project Get(Func<Project, bool> predicate, bool noTracking = false)
    {
        if (noTracking)
        {
            return _dataContext.Projects
                       .AsNoTracking()
                       .Include(project => project.Collaborators)
                       .FirstOrDefault(predicate)
                   ?? throw new IndexOutOfRangeException();
        }
            
        return _dataContext.Projects
                   .Include(project => project.Collaborators)
                   .FirstOrDefault(predicate)
               ?? throw new IndexOutOfRangeException();
    }

    public void Create(Project project)
    {
        _dataContext.Add(project);
        Save();
    }

    public void Update(Project project)
    {
        _dataContext.Update(project);
        Save();
    }

    public void Delete(Project project)
    {
        _dataContext.Remove(project);
        Save();
    }

    public bool Exist(Func<Project, bool> predicate) => _dataContext.Projects.Any(predicate);

    public void Save() => _dataContext.SaveChanges();
}