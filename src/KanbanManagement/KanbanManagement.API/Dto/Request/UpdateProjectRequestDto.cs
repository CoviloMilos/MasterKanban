using System;

namespace KanbanManagement.API.Dto.Request
{
    public class UpdateProjectRequestDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid CreatedBy { get; set; }
    }
}