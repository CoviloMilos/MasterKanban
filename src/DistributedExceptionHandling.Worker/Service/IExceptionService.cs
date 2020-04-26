using System.Threading.Tasks;
using DistributedExceptionHandling.Worker.Model;

namespace DistributedExceptionHandling.Worker.Service
{
    public interface IExceptionService
    {
        bool CommitChanges();
        void InsertExceptionModel(ExceptionModel exceptionModel);
    }
}