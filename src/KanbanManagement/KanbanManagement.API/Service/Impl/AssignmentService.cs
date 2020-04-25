using System.Linq;
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
    public class AssignmentService : IAssignmentService
    {
        private IAssignmentRepository _assignmentRepository;
        private ILoggerManager _logger;
        private IMapper _mapper;

        public AssignmentService(IAssignmentRepository assignmentRepository, ILoggerManager logger, IMapper mapper) 
        {
            _assignmentRepository = assignmentRepository;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<AssignmentResponseDto> CreateAssignment(CreateAssignmentRequestDto createAssignmentRequestDto)
        {
            _logger.LogInfo("Creating new assignment ...");

            var assignment = _mapper.Map<Assignment>(createAssignmentRequestDto);
            await _assignmentRepository.CreateAssignment(assignment);

            if (await _assignmentRepository.SaveAll())
            {
                return _mapper.Map<AssignmentResponseDto>(assignment);
            } 
            else 
            {
                _logger.LogError("Something went wront while trying to create new assignment ...");
                throw new UnknownException("Something went wront while trying to create new assignment.", DomainConsts.APPLICATION_NAME);
            }
        }

        public async Task<EntityDeletedSuccessfully> DeleteById(string guid)
        {
            _logger.LogInfo($"Deleting assignment with id: {guid} ...");

            var assignment = await _assignmentRepository.FindAssignmentById(Utils.checkGuidFormat(guid));

            if (assignment != null) 
            {
                _assignmentRepository.Delete(assignment);

                if (await _assignmentRepository.SaveAll()) 
                    return new EntityDeletedSuccessfully(DomainConsts.ENTITY_ASSIGNMENT, $"Assignment with id: {guid} is deleted successfullty.", HttpStatusCode.OK);
                else 
                {
                    _logger.LogError($"Something went wrong while trying to delete assignment with id: {guid}");
                    throw new UnknownException($"Something went wrong while trying to delete assignment with id: {guid}", DomainConsts.APPLICATION_NAME);
                }

            }
            
            throw new EntityNotFoundException($"Entity with id: {guid} not found.", DomainConsts.APPLICATION_NAME);
        }

        public async Task<IEnumerable<AssignmentResponseDto>> RetrieveAll(string projectId)
        {
            _logger.LogInfo("Retrieving all existing assignments from database ...");

            var assignments = await _assignmentRepository.FindAssignments(Utils.checkGuidFormat(projectId));

            if (assignments.Count() == 0) 
                throw new EntityNotFoundException($"Entity with id: {projectId} not found.", DomainConsts.APPLICATION_NAME);

            return _mapper.Map<IEnumerable<Assignment>, IEnumerable<AssignmentResponseDto>>(assignments);
        }

        public async Task<AssignmentResponseDto> RetrieveById(string guid)
        {
            _logger.LogInfo($"Retrieving assignment with id: {guid}");

            var assignment =  await _assignmentRepository.FindAssignmentById(Utils.checkGuidFormat(guid));

            if (assignment != null)
                return _mapper.Map<AssignmentResponseDto>(assignment);

            throw new EntityNotFoundException($"Entity with id: {guid} not found.", DomainConsts.APPLICATION_NAME);
        }
    }
}