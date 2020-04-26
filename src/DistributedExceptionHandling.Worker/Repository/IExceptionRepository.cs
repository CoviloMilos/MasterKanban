
using System.Threading.Tasks;
using DistributedExceptionHandling.Worker.Model;

namespace DistributedExceptionHandling.Worker.Repository
{
    public interface IExceptionRepository
    {
        Task InsertEntity(ExceptionModel exception);
        bool SaveAll();
    }
}