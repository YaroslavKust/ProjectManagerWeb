using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProjectManager.DAL.UnitOfWorks;
using ProjectManager.Entities.Models;

namespace ProjectManager.API.Controllers
{
    [Route("api/projects/{projectId}/tasks")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private IUnitOfWork _unit;

        public TaskController(IUnitOfWork unit)
        {
            _unit = unit;
        }


        [HttpGet]
        public async Task<IActionResult> GetTasks(int projectId)
        {
            var project = await _unit.Projects.GetProjectAsync(projectId);

            if (project is null)
            {
                return NotFound();
            }
            else
            {
                var tasks = await _unit.Tasks.GetTasksAsync(projectId);
                return Ok(tasks);
            }
        }


        [HttpGet("{id}", Name = "TaskById")]
        public async Task<IActionResult> GetTask(int projectId, int id)
        {
            var project = await _unit.Projects.GetProjectAsync(projectId);

            if (project is null)
            {
                return NotFound();
            }
            else
            {
                var task = await _unit.Tasks.GetTaskAsync(projectId, id);

                if (task is null)
                    return NotFound();

                return Ok(task);
            }
        }


        [HttpPost]
        public async Task<IActionResult> CreateTask(int projectId, [FromBody] MyTask task)
        {
            var project = await _unit.Projects.GetProjectAsync(projectId);

            if (project is null)
            {
                return NotFound();
            }

            task.ProjectId = projectId;

            _unit.Tasks.Add(task);
            await _unit.SaveAsync();

            return CreatedAtRoute("TaskById", new { projectId, id = task.Id }, task);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(int projectId, int id, [FromBody] MyTask task)
        {
            var project = await _unit.Projects.GetProjectAsync(projectId);

            if (project is null)
            {
                return NotFound();
            }

            var oldTask = await _unit.Tasks.GetTaskAsync(projectId, id);

            if (oldTask is null)
                return NotFound();

            oldTask.Description = task.Description;
            oldTask.ProgressInPercents = task.ProgressInPercents;

            _unit.Tasks.Update(oldTask);

            await _unit.SaveAsync();

            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int projectId, int id)
        {
            var project = await _unit.Projects.GetProjectAsync(projectId);

            if (project is null)
            {
                return NotFound();
            }

            var task = await _unit.Tasks.GetTaskAsync(projectId, id);

            if (task is null)
                return NotFound();

            _unit.Tasks.Delete(task);

            await _unit.SaveAsync();

            return NoContent();
        }
    }
}
