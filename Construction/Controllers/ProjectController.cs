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
            //var header = project.headerImage;
            //var pathing = Directory.GetCurrentDirectory();
            //var image = System.IO.File.OpenRead(header);
            //project.headerImage = Path.Combine(
            //   Directory.GetCurrentDirectory(), "wwwroot",
            //   project.headerImage.Split("\\")[project.headerImage.Split("\\").Length-1]);
            //project.ProsureImage = _hostingEnv.WebRootPath +  project.ProsureImage.Split("\\")[project.ProsureImage.Split("\\").Length - 1];
            //return project;

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
                project.description = projectVM.description;
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
                    formFile.CopyToAsync(fileStream);
                }
            return filePath;
            }
            return null ;
        }
    
        [HttpPut("{id}")]
        public async Task<ActionResult> PutProject(int id,[FromForm] ProjectViewModel projectVM)
        {
            var projectToCheck = await _ProjectRepository.Get(id);
            Project project = new Project();
            if(projectToCheck==null)
            {
                return BadRequest("Id doesn't match");
            }
            if (projectVM.headerImage != null)
            {
                projectToCheck.headerImage = UploadedFile(projectVM.headerImage);
            }
            if (projectVM.ProSureImage != null)
            {
                projectToCheck.ProsureImage = UploadedFile(projectVM.ProSureImage);
            }
            projectToCheck.closed = projectVM.closed;
           projectToCheck.description = projectVM.description;
            projectToCheck.location = projectVM.location;
            projectToCheck.numberOfBuildings = projectVM.numberOfBuildings;
            projectToCheck.numberOfUnits = projectVM.numberOfUnits;
            projectToCheck.projectName = projectVM.projectName;
                //var newProject = await _ProjectRepository.Create(project);
             await _ProjectRepository.Update(projectToCheck);
                return NoContent();
            //else
            //{
            //    return BadRequest();
            //}
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
