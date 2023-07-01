using System.Collections.Generic;
using BLL.DTO;
using BLL.Services;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet]
        public IEnumerable<TaskDto> Get() => _taskService.GetAll();

        [HttpPost]
        public IActionResult CreateTask([FromBody] TaskDto taskDto)
        {
            if (taskDto == null || ModelState.IsValid == false)
                return BadRequest(ModelState);

            _taskService.Add(taskDto);
            return Ok();
        }

        [HttpGet("detail/{id:int}")]
        public TaskDto GetTask(int id) => _taskService.Get(task => task.Id == id);

        [HttpPut("edit/{id:int}")]
        public IActionResult UpdateTask(int id, [FromBody] TaskDto taskDto)
        {
            if (taskDto == null || ModelState.IsValid == false)
                return BadRequest();

            if (_taskService.Exist(task => task.Id == id) == false)
                return NotFound();

            _taskService.Update(taskDto);
            return NoContent();
        }

        [HttpDelete("delete/{id:int}")]
        public IActionResult DeleteTask(int id, [FromBody] TaskDto taskDto)
        {
            if (taskDto == null)
                return BadRequest();
            
            if (_taskService.Exist(task => task.Id == id) == false)
                return NotFound();
            
            _taskService.Delete(taskDto);
            return NoContent();
        }
    }
}