using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using KanbanManagement.API.Model;

namespace KanbanManagement.API.Repository
{
    public interface IProjectRepository : IBaseRepository<Project>
    {
        Task CreateProject(Project project);
        Task<IEnumerable<Project>> FindProjects();
        Task<Project> FindProjectById(Guid guid);
    }
}