using System;
using System.ComponentModel.DataAnnotations;

namespace KanbanManagement.API.Dto.Request
{
    public class CreateAssignmentRequestDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public Guid CreatedBy { get; set; }
        [Required]
        public Guid AssignedTo { get; set; }
        [Required]
        public string Status { get; set; }
        [Required]
        public Guid ProjectId { get; set; }
    }
}