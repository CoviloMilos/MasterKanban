using System;
using System.Threading.Tasks;
using Common.ExceptionHanding.Exceptions;
using DistributedExceptionHandling.Worker.Consts;
using DistributedExceptionHandling.Worker.Model;

namespace DistributedExceptionHandling.Worker.Repository.Impl
{
    public class ExceptionRepository : IExceptionRepository
    {
        private DataContext _context;

        public ExceptionRepository(DataContext context) {
            _context = context;
        }

        public async Task InsertEntity(ExceptionModel exception)
        {
            await _context.AddAsync(exception);
        }

        public bool SaveAll()
        {
            try
            {
              return _context.SaveChanges() > 0;   
            }
            catch (Exception e)
            {
                throw new UnknownException(e.Message, DomainConsts.APPLICATION_NAME);
            }
        }
    }
}