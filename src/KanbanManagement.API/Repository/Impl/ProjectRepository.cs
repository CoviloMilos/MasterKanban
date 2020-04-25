using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.ExceptionHanding.Exceptions;
using KanbanManagement.API.Consts;
using KanbanManagement.API.Model;
using Microsoft.EntityFrameworkCore;

namespace KanbanManagement.API.Repository.Impl
{
    public class ProjectRepository : IProjectRepository
    {
        private DataContext _context;

        public ProjectRepository(DataContext context) {
            _context = context;
        }

        public async Task CreateProject(Project project)
        {
            await _context.AddAsync(project);
        }

        public void Delete(Project entity)
        {
            _context.Remove(entity);
        }

        public async Task<Project> FindProjectById(Guid guid)
        {
            return await _context.Projects
                        .Include(p => p.Assignments)
                        .FirstOrDefaultAsync(p => p.Id == guid);
        }

        public async Task<IEnumerable<Project>> FindProjects()
        {
            return await _context.Projects
                        .Include(p => p.Assignments)
                        .OrderBy(p => p.DateOfCreation)
                        .ToListAsync();
        }
        public async Task<bool> SaveAll()
        {
            
            try
            {
              return await _context.SaveChangesAsync() > 0;   
            }
            catch (Exception e)
            {
                throw new UnknownException(e.Message, DomainConsts.APPLICATION_NAME);
            }
        }
    }
}