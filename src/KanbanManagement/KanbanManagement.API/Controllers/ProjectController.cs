using System.Threading.Tasks;
using Common.ExceptionHanding.Exceptions;
using KanbanManagement.API.Dto.Request;
using KanbanManagement.API.Service;
using Microsoft.AspNetCore.Mvc;

namespace KanbanManagement.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private IProjectService _projectService;

        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateProject([FromBody] CreateProjectRequestDto createProjectRequestDto) 
        {
            return Ok(await _projectService.CreateProject(createProjectRequestDto));
        }

        [HttpGet]
        public async Task<IActionResult> RetrieveAllProjects() 
        {
            return Ok(await _projectService.RetrieveAll());
        }

        [HttpGet("{projectId}", Name = "RetrieveProjectById")]
        public async Task<IActionResult> RetrieveProjectById(string projectId) 
        {
            return Ok(await _projectService.RetrieveById(projectId));
        }

        [HttpDelete("{projectId}", Name = "DeleteProjectById")]
        public async Task<IActionResult> DeleteProjectById(string projectId) 
        {
            return Ok(await _projectService.DeleteById(projectId));
        }

        [HttpPut("{projectId}", Name = "UpdateProjectById")]
        public async Task<IActionResult> UpdateProjectById(UpdateProjectRequestDto updateProjectRequestDto, string projectId) 
        {
            return Ok(await _projectService.UpdateProject(updateProjectRequestDto, projectId));
        }
    }
}