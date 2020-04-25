using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using KanbanManagement.API.Model;

namespace KanbanManagement.API.Repository
{
    public interface IAssignmentRepository : IBaseRepository<Assignment>
    {
        Task CreateAssignment(Assignment assignment);
        Task<IEnumerable<Assignment>> FindAssignments(Guid projectId);
        Task<Assignment> FindAssignmentById(Guid guid);
    }
}