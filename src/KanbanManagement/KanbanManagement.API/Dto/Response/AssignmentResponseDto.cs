using System;

namespace KanbanManagement.API.Dto.Response
{
    public class AssignmentResponseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DateOfCreation { get; set; }
        public Guid CreatedBy { get; set; }
        public Guid AssignedTo { get; set; }
        public string Status { get; set; }
    }
}