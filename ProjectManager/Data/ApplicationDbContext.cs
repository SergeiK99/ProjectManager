using Microsoft.EntityFrameworkCore;
using ProjectManager.Models;

namespace ProjectManager.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Employee> Employee { get; set; }
        public DbSet<Project> Project { get; set; }
        public DbSet<ProjectEmployee> ProjectEmployee { get; set; }
        public DbSet<ExecutorCompany> ExecutorCompany { get; set; }
        public DbSet<ClientCompany> ClientCompany { get; set; }
    }
}
