using Construction.Models;
using Construction.Repositories;
using Construction.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Linq;
using System.Threading.Tasks;

namespace Construction.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ProjectController : ControllerBase
    {
        private readonly IProject _ProjectRepository;
        private readonly IHostingEnvironment _hostingEnv;


        public ProjectController(IProject project, IHostingEnvironment hostingEnv)
        {
            _ProjectRepository = project;
            _hostingEnv = hostingEnv;

        }
        [HttpGet]
        public async Task<IEnumerable<Project>> GetProjects()
        {
            return await _ProjectRepository.Get();
        }
        [HttpGet("{id}")]
       public async  Task<ActionResult<Project>> GetProjects(int id)
        {
            return await _ProjectRepository.Get(id);
        }
        [HttpPost]
        public async Task<ActionResult<Project>> CreateProject([FromForm] ProjectViewModel projectVM)
        {
            if (projectVM.headerImage != null)
            {
                var a = _hostingEnv.WebRootPath;
                Project project = new Project();
                project.closed = projectVM.closed;
                project.headerImage =    UploadedFile(projectVM.headerImage);  //save the filePath to database ImagePath field.
                project.ProsureImage = UploadedFile(projectVM.ProSureImage);  //save the filePath to database ImagePath field.
                project.location = projectVM.location;
                project.numberOfBuildings = projectVM.numberOfBuildings;
                project.numberOfUnits = projectVM.numberOfUnits;
                project.projectName = projectVM.projectName;
                var newProject = await _ProjectRepository.Create(project);
                return CreatedAtAction((nameof(GetProjects)), new { id = newProject.Id }, newProject);
            }
            else
            {
                return BadRequest();
            }
           
        }
        private string UploadedFile(IFormFile formFile)
        {
            string uniqueFileName = null;

            if (formFile != null)
            {
                string uploadsFolder = Path.Combine(_hostingEnv.WebRootPath, "Resources");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + formFile.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    formFile.CopyTo(fileStream);
                }
           
            return filePath;
            }
            return null ;
        }
    
    [HttpPut("{id}")]
        public async Task<ActionResult> PutProject(int id,[FromBody] ProjectViewModel projectVM)
        {
            if(id!=projectVM.Id)
            {
                return BadRequest("Id doesn't match");
            }
            if (projectVM.headerImage != null)
            {
                var a = _hostingEnv.WebRootPath;
                var fileName = Path.GetFileName(projectVM.headerImage.FileName);
                var filePath = Path.Combine("Resources\\Images", fileName);

                using (var fileSteam = new FileStream(filePath, FileMode.Create))
                {
                    await projectVM.headerImage.CopyToAsync(fileSteam);
                }
                var fileName2 = Path.GetFileName(projectVM.ProSureImage.FileName);
                var filePath2 = Path.Combine("Resources\\Images", fileName);
                using (var fileSteam = new FileStream(filePath2, FileMode.Create))
                {
                    await projectVM.ProSureImage.CopyToAsync(fileSteam);
                }

                Project project = new Project();
                project.closed = projectVM.closed;
                project.headerImage = filePath;  //save the filePath to database ImagePath field.
                project.ProsureImage = filePath2;  //save the filePath to database ImagePath field.
                project.location = projectVM.location;
                project.numberOfBuildings = projectVM.numberOfBuildings;
                project.numberOfUnits = projectVM.numberOfUnits;
                project.projectName = projectVM.projectName;
                var newProject = await _ProjectRepository.Create(project);
                await _ProjectRepository.Update(project);
                return NoContent();
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpDelete("{id}")]

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
