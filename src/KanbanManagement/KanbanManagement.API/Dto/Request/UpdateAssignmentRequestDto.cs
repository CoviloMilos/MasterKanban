using System;

namespace KanbanManagement.API.Dto.Request
{
    public class UpdateAssignmentRequestDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid CreatedBy { get; set; }
        public Guid AssignedTo { get; set; }
        public string Status { get; set; }
    }
}