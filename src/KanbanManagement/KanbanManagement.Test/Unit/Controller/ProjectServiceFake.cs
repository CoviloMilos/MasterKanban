using System.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net;
using System.Threading.Tasks;
using Common.Dto;
using KanbanManagement.API.Consts;
using KanbanManagement.API.Dto.Request;
using KanbanManagement.API.Dto.Response;
using KanbanManagement.API.Service;

namespace KanbanManagement.Test.Unit.Controller
{
    public class ProjectServiceFake : IProjectService
    {
        private readonly List<ProjectResponseDto> _projects;

        public ProjectServiceFake()
        {
            ProjectResponseDto project = new ProjectResponseDto();
            project.Id = Guid.Parse("ab2bd817-98cd-4cf3-a80a-53ea0cd9c200");
            project.Name = "Name";
            project.Description = "Description";
            project.CreatedBy = Guid.Parse("ab2bd817-98cd-4cf3-a80a-53ea0cd9c100");
            project.DateOfCreation = DateTime.Now;

            _projects = new List<ProjectResponseDto>();
            _projects.Add(project);
        }
        
        public async Task<ProjectResponseDto> CreateProject(CreateProjectRequestDto createProjectRequestDto)
        {
            await Task.Delay(10);

            return _projects.ElementAt(0);
        }

        public async Task<EntityDeletedSuccessfully> DeleteById(string guid)
        {
            await Task.Delay(10);

            return new EntityDeletedSuccessfully(DomainConsts.ENTITY_PROJECT, $"Project with id: {guid} is deleted successfullty.", HttpStatusCode.OK);
        }

        public async Task<IEnumerable<ProjectResponseDto>> RetrieveAll()
        {
            await Task.Delay(10);

            return _projects;
        }

        public async Task<ProjectByIdResponseDto> RetrieveById(string guid)
        {
            ProjectByIdResponseDto project = new ProjectByIdResponseDto();
            project.Id = Guid.Parse("ab2bd817-98cd-4cf3-a80a-53ea0cd9c200");
            project.Name = "Name";
            project.Description = "Description";
            project.CreatedBy = Guid.Parse("ab2bd817-98cd-4cf3-a80a-53ea0cd9c100");
            project.DateOfCreation = DateTime.Now;
            project.Assignments = new Collection<AssignmentResponseDto>();
            
            await Task.Delay(10);
        
            return project;
        }
    }
}