using Construction.Models;
using Construction.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Construction.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ProjectController : ControllerBase
    {
        private readonly IProject _ProjectRepository;

        public ProjectController(IProject project)
        {
            _ProjectRepository = project;
        }
        [HttpGet]
        public async Task<IEnumerable<PoneNumber>> GetProjects()
        {
            return await _ProjectRepository.Get();
        }
        [HttpGet("{id}")]
       public async  Task<ActionResult<PoneNumber>> GetProjects(int id)
        {
            return await _ProjectRepository.Get(id);
        }
        [HttpPost]
        public async Task<ActionResult<PoneNumber>> CreateProject([FromBody] PoneNumber project)
        {
            var newProject = await _ProjectRepository.Create(project);
            return CreatedAtAction((nameof(GetProjects)), new { id = newProject.Id }, newProject);
        }
        [HttpPut]

        public async Task<ActionResult> PutProject(int id,[FromBody] PoneNumber project)
        {
            if(id!=project.Id)
            {
                return BadRequest("Id doesn't match");
            }
            await _ProjectRepository.Update(project);
            return NoContent();
        }
        [HttpDelete]

        public async Task<ActionResult> DeleteProject(int id)
        {
            var project = await _ProjectRepository.Get(id);
            if (project == null)
            {
                return BadRequest("Project Not Found in Our DB");
            }
            await _ProjectRepository.Delete(id);
            return NoContent();
        }


    }
}
