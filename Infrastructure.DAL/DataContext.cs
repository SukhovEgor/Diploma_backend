using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DAL
{
    public class DataContext : DbContext
    {
        public DbSet<CalculationEntity> Calculations { get; set; } = null!;
        public DbSet<CalculationResultEntity> CalculationResults { get; set; } = null!;

        public DataContext() => Database.EnsureCreated();

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CalculationEntity>().HasKey(c => new { c.Id });
            modelBuilder.Entity<CalculationResultEntity>().HasKey(r => new { r.CalculationId });
        }
    }
}
