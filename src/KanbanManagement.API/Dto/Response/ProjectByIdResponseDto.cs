using System;
using System.Collections.Generic;

namespace KanbanManagement.API.Dto.Response
{
    public class ProjectByIdResponseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DateOfCreation { get; set; }
        public Guid CreatedBy { get; set; }
        public ICollection<AssignmentResponseDto> Assignments { get; set; }
    }
}