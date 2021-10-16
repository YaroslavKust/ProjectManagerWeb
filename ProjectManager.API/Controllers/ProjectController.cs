using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectManager.DAL.UnitOfWorks;
using ProjectManager.Entities.Models;
using System.Threading.Tasks;
using AutoMapper;
using ProjectManager.Entities.DTO;

namespace ProjectManager.API.Controllers
{
    [Route("api/projects")]
    [ApiController]
    [Authorize]
    public class ProjectController : ControllerBase
    {
        private IUnitOfWork _unit;
        private IMapper _mapper;

        public ProjectController(IUnitOfWork unit, IMapper mapper)
        {
            _unit = unit;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<IActionResult> GetProjects()
        {
            var userId = GetUserId();

            var projects = await _unit.Projects.GetProjectsAsync(p=>p.UserId == userId);
            var results = _mapper.Map<IEnumerable<ProjectDto>>(projects);

            return Ok(results);
        }


        [HttpGet("{id}", Name = "ProjectById")]
        public async Task<IActionResult> GetProjectById(int id)
        {
            var userId = GetUserId();

            var project = await _unit.Projects.GetProjectAsync(userId, id);
            var result = _mapper.Map<ProjectDto>(project);

            return Ok(result);
        }


        [HttpPost]
        public async Task<IActionResult> CreateProject([FromBody] ProjectForManipulation project)
        {
            var result = _mapper.Map<Project>(project);
            result.UserId = GetUserId();
            _unit.Projects.Add(result);
            await _unit.SaveAsync();
            return CreatedAtRoute("ProjectById", new { id = result.Id }, _mapper.Map<ProjectDto>(result));
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProject(int id,[FromBody] ProjectForManipulation project)
        {
            var userId = GetUserId();
            var updateProject = await _unit.Projects.GetProjectAsync(userId, id);

            if (updateProject is null)
                return NotFound();

            _mapper.Map(project, updateProject);

            _unit.Projects.Update(updateProject);
            await _unit.SaveAsync();

            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            var userId = GetUserId();
            var delProject = await _unit.Projects.GetProjectAsync(userId, id);

            if (delProject is null)
                return NotFound();

            _unit.Projects.Delete(delProject);
            await _unit.SaveAsync();

            return NoContent();
        }


        private int GetUserId()
        {
            return Int32.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
        }
    }
}
