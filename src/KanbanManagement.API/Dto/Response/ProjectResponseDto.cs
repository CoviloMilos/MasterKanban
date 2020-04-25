using System;
using System.Collections.Generic;
using KanbanManagement.API.Model;

namespace KanbanManagement.API.Dto.Response
{
    public class ProjectResponseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DateOfCreation { get; set; }
        public Guid CreatedBy { get; set; }
    }
}