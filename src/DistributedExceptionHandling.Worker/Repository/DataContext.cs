using DistributedExceptionHandling.Worker.Model;
using Microsoft.EntityFrameworkCore;

namespace DistributedExceptionHandling.Worker.Repository
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options) {}

        public DbSet<ExceptionModel> ExceptionModels { get; set; }

    }
}