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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
                .HasIndex(e => e.RegistrationNumber)
                .IsUnique();
        }
    }
}
