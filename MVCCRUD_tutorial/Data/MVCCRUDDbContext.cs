using Microsoft.EntityFrameworkCore;
using MVCCRUD_tutorial.Models.Domain;

namespace MVCCRUD_tutorial.Data
{
    public class MVCCRUDDbContext : DbContext
    {
        public MVCCRUDDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
    }
}
