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
    public class AssignmentRepository : IAssignmentRepository
    {
        private DataContext _context;

        public AssignmentRepository(DataContext context) {
            _context = context;
        }

        public async Task CreateAssignment(Assignment assignment)
        {
            await _context.AddAsync(assignment);
        }

        public void Delete(Assignment entity)
        {
            _context.Remove(entity);
        }

        public async Task<Assignment> FindAssignmentById(Guid guid)
        {
            return await _context.Assignments.FirstOrDefaultAsync(p => p.Id == guid);
        }

        public async Task<IEnumerable<Assignment>> FindAssignments(Guid projectId)
        {
            return await _context.Assignments
                            .Where(a => a.ProjectId == projectId)
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