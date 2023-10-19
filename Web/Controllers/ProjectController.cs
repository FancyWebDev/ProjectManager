using BLL.DTO;
using BLL.Services;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[ApiController]
[Route("[controller]")]
public class ProjectController : ControllerBase
{
    private readonly IProjectService _projectService;

    public ProjectController(IProjectService projectService)
    {
        _projectService = projectService;
    }

    [HttpGet]
    public IEnumerable<ProjectDto> Get() => _projectService.GetAll();

    [HttpPost]
    public async Task<IActionResult> CreateProject([FromForm] ProjectDto project)
    {
        if (project == null || ModelState.IsValid == false)
            return BadRequest(ModelState);

        await _projectService.Add(project);
        return Ok();
    }

    [HttpGet("detail/{id:int}")]
    public ProjectDto GetProject(int id) => _projectService.Get(project => project.Id == id);

    [HttpPut("edit/{id:int}")]
    public IActionResult UpdateProject(int id, [FromBody] ProjectDto projectDto)
    {
        if (projectDto == null || ModelState.IsValid == false)
            return BadRequest();

        if (_projectService.Exist(project => project.Id == id) == false)
            return NotFound();

        _projectService.Update(projectDto);
        return NoContent();
    }

    [HttpDelete("delete/{id:int}")]
    public IActionResult DeleteProject(int id, [FromBody] ProjectDto projectDto)
    {
        if (projectDto == null)
            return BadRequest();
            
        if (_projectService.Exist(project => project.Id == id) == false)
            return NotFound();
            
        _projectService.Delete(projectDto);
        return NoContent();
    }
}