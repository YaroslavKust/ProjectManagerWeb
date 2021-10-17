using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectManager.API.ActionFilters;
using ProjectManager.DAL.UnitOfWorks;
using ProjectManager.Entities.DTO;
using ProjectManager.Entities.Models;

namespace ProjectManager.API.Controllers
{
    [Route("api/projects/{projectId}/tasks")]
    [ApiController]
    [Authorize]
    [ServiceFilter(typeof(ValidateProjectExistsAttribute))]
    public class TaskController : ControllerBase
    {
        private IUnitOfWork _unit;
        private IMapper _mapper;

        public TaskController(IUnitOfWork unit, IMapper mapper)
        {
            _unit = unit;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<IActionResult> GetTasks(int projectId)
        {
            var tasks = await _unit.Tasks.GetTasksAsync(projectId);
            var results = _mapper.Map<IEnumerable<TaskDto>>(tasks);

            return Ok(results);
        }


        [HttpGet("{id}", Name = "TaskById")]
        [ServiceFilter(typeof(ValidateTaskExistsAttribute))]
        public IActionResult GetTask()
        {
            var task = HttpContext.Items["SelectedTask"] as MyTask;

            var result = _mapper.Map<TaskDto>(task);
                
            return Ok(result);
        }


        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> CreateTask(int projectId, [FromBody] TaskForCreate task)
        {
            var result = _mapper.Map<MyTask>(task);
            result.ProjectId = projectId;

            _unit.Tasks.Add(result);
            await _unit.SaveAsync();

            return CreatedAtRoute("TaskById", new { projectId, id = result.Id }, _mapper.Map<TaskDto>(result));
        }


        [HttpPut("{id}")]
        [ValidateModel]
        [ServiceFilter(typeof(ValidateTaskExistsAttribute))]
        public async Task<IActionResult> UpdateTask(int projectId, int id, [FromBody] TaskForUpdate task)
        {
            var oldTask = HttpContext.Items["SelectedTask"] as MyTask;

            _mapper.Map(task, oldTask);

            _unit.Tasks.Update(oldTask);
            await _unit.SaveAsync();

            return NoContent();
        }


        [HttpDelete("{id}")]
        [ServiceFilter(typeof(ValidateTaskExistsAttribute))]
        public async Task<IActionResult> DeleteTask(int projectId, int id)
        {
            var task = HttpContext.Items["SelectedTask"] as MyTask;

            _unit.Tasks.Delete(task);
            await _unit.SaveAsync();

            return NoContent();
        }
    }
}
