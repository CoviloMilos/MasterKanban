using System.Threading.Tasks;
using KanbanManagement.API.Dto.Request;
using KanbanManagement.API.Service;
using Microsoft.AspNetCore.Mvc;

namespace KanbanManagement.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AssignmentController : ControllerBase
    {
        private IAssignmentService _assignmentService;

        public AssignmentController(IAssignmentService assignmentService)
        {
            _assignmentService = assignmentService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAssignment([FromBody] CreateAssignmentRequestDto createAssignmentRequestDto) 
        {
            return Ok(await _assignmentService.CreateAssignment(createAssignmentRequestDto));
        }

        [HttpGet("project/{projectId}")]
        public async Task<IActionResult> RetrieveAllAssignments(string projectId) 
        {
            return Ok(await _assignmentService.RetrieveAll(projectId));
        }

        [HttpGet("{assignmentId}", Name = "RetrieveAssignmentById")]
        public async Task<IActionResult> RetrieveAssignmentById(string assignmentId) 
        {
            return Ok(await _assignmentService.RetrieveById(assignmentId));
        }

        [HttpDelete("{assignmentId}", Name = "DeleteAssignmentById")]
        public async Task<IActionResult> DeleteAssignmentById(string assignmentId) 
        {
            return Ok(await _assignmentService.DeleteById(assignmentId));
        }

        [HttpPut("{assignmentId}/project/{projectId}", Name = "UpdateAssignmentById")]
        public async Task<IActionResult> UpdateAssignmentById(UpdateAssignmentRequestDto updateAssignmentRequestDto, string assignmentId, string projectId) 
        {
            return Ok(await _assignmentService.UpdateAssignment(updateAssignmentRequestDto, assignmentId, projectId));
        }
    }
}