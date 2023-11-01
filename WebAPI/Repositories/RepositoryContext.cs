using Microsoft.EntityFrameworkCore;
using WebAPI.Entities;

namespace WebAPI.Repositories
{
    public class RepositoryContext : DbContext
    {
        public RepositoryContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Employee> Employees { get; set; }
    }
}
