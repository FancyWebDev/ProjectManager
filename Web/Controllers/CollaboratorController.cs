using System.Collections.Generic;
using BLL.DTO;
using BLL.Services;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CollaboratorController : ControllerBase
    {
        private readonly ICollaboratorService _collaboratorService;

        public CollaboratorController(ICollaboratorService collaboratorService)
        {
            _collaboratorService = collaboratorService;
        }

        [HttpGet]
        public IEnumerable<CollaboratorDto> Get() => _collaboratorService.GetAll();

        [HttpGet("detail/{id}")]
        public CollaboratorDto GetCollaborator(string id) => _collaboratorService.Get(collaborator => collaborator.Id == id);

        [HttpPut("edit/{id}")]
        public IActionResult UpdateCollaborator(string id, [FromBody] CollaboratorDto collaboratorDto)
        {
            if (collaboratorDto == null || ModelState.IsValid == false)
                return BadRequest();

            if (_collaboratorService.Exist(collaborator => collaborator.Id == id) == false)
                return NotFound();

            _collaboratorService.Update(collaboratorDto);
            return NoContent();
        }

        [HttpDelete("delete/{id}")]
        public IActionResult DeleteCollaborator(string id, [FromBody] CollaboratorDto collaboratorDto)
        {
            if (collaboratorDto == null)
                return BadRequest();
            
            if (_collaboratorService.Exist(collaborator => collaborator.Id == id) == false)
                return NotFound();
            
            _collaboratorService.Delete(collaboratorDto);
            return NoContent();
        }
    }
}