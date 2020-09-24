using Microsoft.EntityFrameworkCore;
using DesafioMedicos.Models;

namespace DesafioMedicos.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        public DbSet<Doctors> Doctors { get; set; }
        public DbSet<User> Users { get; set; }
    }
}