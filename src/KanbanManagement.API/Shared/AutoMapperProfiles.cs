using AutoMapper;
using KanbanManagement.API.Dto.Request;
using KanbanManagement.API.Dto.Response;
using KanbanManagement.API.Model;

namespace KanbanManagement.API.Shared
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
        
            CreateMap<CreateAssignmentRequestDto, Assignment>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => Utils.convertStringToAssigmentStatusEnum(src.Status)));
            CreateMap<Assignment, AssignmentResponseDto>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => Utils.convertAssigmentStatusEnumToString(src.Status)));
            
            CreateMap<CreateProjectRequestDto, Project>();
            CreateMap<Project, ProjectResponseDto>();
            CreateMap<Project, ProjectByIdResponseDto>();
        }
    }
}