using Microsoft.AspNetCore.Mvc;
using ProjectManager.DAL.UnitOfWorks;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using ProjectManager.DAL.Models;
using ProjectManager.Entities.Models;

namespace ProjectManager.API.Controllers
{
    [Route("api/projects")]
    [ApiController]
    [Authorize]
    public class ProjectController : ControllerBase
    {
        private IUnitOfWork _unit;

        public ProjectController(IUnitOfWork unit)
        {
            _unit = unit;
        }

        [HttpGet]
        public async Task<IActionResult> GetProjects()
        {
            var projects = await _unit.Projects.GetProjectsAsync(null);
            return Ok(projects);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetProjectById(int id)
        {
            var project = await _unit.Projects.GetProjectAsync(id);
            return Ok(project);
        }


        [HttpPost]
        public async Task<IActionResult> CreateProject([FromBody] Project project)
        {
            _unit.Projects.Add(project);
            await _unit.SaveAsync();
            return Ok();
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProject(int id,[FromBody] Project project)
        {
            var updateProject = await _unit.Projects.GetProjectAsync(project.Id);

            if (updateProject is null)
                return NotFound();

            updateProject.Name = project.Name;

            _unit.Projects.Update(updateProject);
            await _unit.SaveAsync();

            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            var delProject = await _unit.Projects.GetProjectAsync(id);

            if (delProject is null)
                return NotFound();

            _unit.Projects.Delete(delProject);
            await _unit.SaveAsync();

            return NoContent();
        }
    }
}
