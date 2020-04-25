using System.Threading.Tasks;

namespace KanbanManagement.API.Repository
{
    public interface IBaseRepository<T>
    {
        void Delete (T entity);
        Task<bool> SaveAll();
    }
}