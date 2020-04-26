using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Dto;
using KanbanManagement.API.Dto.Request;
using KanbanManagement.API.Dto.Response;

namespace KanbanManagement.API.Service
{
    public interface IAssignmentService
    {
        Task<AssignmentResponseDto> CreateAssignment(CreateAssignmentRequestDto createAssignmentRequestDto);
        Task<IEnumerable<AssignmentResponseDto>> RetrieveAll(string projectGuid);
        Task<AssignmentResponseDto> RetrieveById(string guid);
        Task<EntityDeletedSuccessfully> DeleteById(string guid);
        Task<AssignmentResponseDto> UpdateAssignment(UpdateAssignmentRequestDto assignmentRequestDto, string guid, string projectGuid);
    }
}