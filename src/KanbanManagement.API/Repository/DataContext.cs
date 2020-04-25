using Microsoft.EntityFrameworkCore;
using KanbanManagement.API.Model;

namespace KanbanManagement.API.Repository
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options) {}

        public DbSet<Project> Projects { get; set; }
        public DbSet<Assignment> Assignments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Assignment>()
                        .HasOne(a => a.Project)
                        .WithMany(p => p.Assignments)
                        .HasForeignKey(a => a.ProjectId)
                        .HasPrincipalKey(p => p.Id);
        }
    }
}