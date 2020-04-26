using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Dto;
using KanbanManagement.API.Dto.Request;
using KanbanManagement.API.Dto.Response;

namespace KanbanManagement.API.Service
{
    public interface IProjectService
    {
        Task<ProjectResponseDto> CreateProject(CreateProjectRequestDto createProjectRequestDto);
        Task<IEnumerable<ProjectResponseDto>> RetrieveAll();
        Task<ProjectByIdResponseDto> RetrieveById(string guid);
        Task<EntityDeletedSuccessfully> DeleteById(string guid);
        Task<ProjectResponseDto> UpdateProject(UpdateProjectRequestDto updateProjectRequestDto, string guid);
    }
}