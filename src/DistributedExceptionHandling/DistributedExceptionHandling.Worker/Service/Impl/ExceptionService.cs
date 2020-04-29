using System.Threading.Tasks;
using AutoMapper;
using Common.Logging;
using DistributedExceptionHandling.Worker.Model;
using DistributedExceptionHandling.Worker.Repository;

namespace DistributedExceptionHandling.Worker.Service.Impl
{
    public class ExceptionService: IExceptionService
    {
        private IExceptionRepository _exceptionRepository;
        private ILoggerManager _logger;

        public ExceptionService(IExceptionRepository exceptionRepository, ILoggerManager logger) 
        {
            _exceptionRepository = exceptionRepository;
            _logger = logger;
        }

        public bool CommitChanges()
        {
            if (_exceptionRepository.SaveAll())
            {
                _logger.LogInfo("Distributed exception inserted successfully...");
                return true;
            } 
            else 
            {
                _logger.LogError("Something went wront while trying to insert distributed exception ...");
                return false;
            }
        }

        public void InsertExceptionModel(ExceptionModel exceptionModel)
        {
            _logger.LogInfo("Interting new distributed exception ...");
            _exceptionRepository.InsertEntity(exceptionModel);
        }

        

    }
}