using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Common.Dto;
using Common.ExceptionHanding.Exceptions;
using Common.ExceptionHandling.Exceptions;
using Common.Logging;
using KanbanManagement.API.Consts;
using KanbanManagement.API.Dto.Request;
using KanbanManagement.API.Dto.Response;
using KanbanManagement.API.Model;
using KanbanManagement.API.Repository;
using KanbanManagement.API.Shared;

namespace KanbanManagement.API.Service.Impl
{
    public class ProjectService : IProjectService
    {
        private IProjectRepository _projectRepository;
        private ILoggerManager _logger;
        private IMapper _mapper;

        public ProjectService(IProjectRepository projectRepository, ILoggerManager logger, IMapper mapper) 
        {
            _projectRepository = projectRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<ProjectResponseDto> CreateProject(CreateProjectRequestDto createProjectRequestDto)
        {
            _logger.LogInfo("Creating new project ...");

            var project = _mapper.Map<Project>(createProjectRequestDto);
            await _projectRepository.CreateProject(project);

            if (await _projectRepository.SaveAll())
            {
                return _mapper.Map<ProjectResponseDto>(project);
            } 
            else 
            {
                _logger.LogError("Something went wront while trying to create new project ...");
                throw new UnknownException("Something went wront while trying to create new project.", DomainConsts.APPLICATION_NAME);
            }
        }

        public async Task<EntityDeletedSuccessfully> DeleteById(string guid)
        {
            _logger.LogInfo($"Deleting project with id: {guid} ...");

            var project = await _projectRepository.FindProjectById(Utils.checkGuidFormat(guid));

            if (project != null) 
            {
                _projectRepository.Delete(project);

                if (await _projectRepository.SaveAll()) 
                    return new EntityDeletedSuccessfully(DomainConsts.ENTITY_PROJECT, $"Project with id: {guid} is deleted successfullty.", HttpStatusCode.OK);
                else 
                {
                    _logger.LogError($"Something went wrong while trying to delete project with id: {guid}");
                    throw new UnknownException($"Something went wrong while trying to delete project with id: {guid}", DomainConsts.APPLICATION_NAME);
                }

            }
            
            throw new EntityNotFoundException($"Entity with id: {guid} not found.", DomainConsts.APPLICATION_NAME);
        }

        public async Task<IEnumerable<ProjectResponseDto>> RetrieveAll()
        {
            _logger.LogInfo("Retrieving all existing projects from database ...");

            var projects = await _projectRepository.FindProjects();
            return _mapper.Map<IEnumerable<Project>, IEnumerable<ProjectResponseDto>>(projects);
        }

        public async Task<ProjectByIdResponseDto> RetrieveById(string guid)
        {
            _logger.LogInfo($"Retrieving project with id: {guid}");

            var project =  await _projectRepository.FindProjectById(Utils.checkGuidFormat(guid));

            if (project != null)
                return _mapper.Map<ProjectByIdResponseDto>(project);

            throw new EntityNotFoundException($"Entity with id: {guid} not found.", DomainConsts.APPLICATION_NAME);
        }
    }
}