using System;
using System.ComponentModel.DataAnnotations;

namespace KanbanManagement.API.Dto.Request
{
    public class CreateProjectRequestDto
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public Guid CreatedBy { get; set; }
    }
}