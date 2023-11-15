using Microsoft.EntityFrameworkCore;
using Infrastructure.DAL.Models;

namespace Infrastructure.DAL
{
    public class DataContext : DbContext
    {
        public DbSet<CalculationProbability> CalculationProbabilities { get; set; }
        public DbSet<ImplementationProbability> ImplementationProbabilities { get; set; }

        //public DataContext() => Database.EnsureCreated();

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            //Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CalculationProbability>().HasKey(cp => new { cp.CalculationId });
            modelBuilder.Entity<ImplementationProbability>().HasKey(ip => new { ip.ImplementationId });
        }
    }
}
