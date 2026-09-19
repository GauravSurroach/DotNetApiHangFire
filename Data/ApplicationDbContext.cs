using Microsoft.EntityFrameworkCore;
using DotNetApiHangFire.Models;

namespace DotNetApiHangFire.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Employees> Employees { get; set; } = null!;
        public DbSet<DotNetApiHangFire.Models.DailyEmployeeAudit> DailyEmployeeAudits { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Employees>(entity =>
            {
                entity.ToTable("Employees");
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Name).IsRequired();
                entity.Property(e => e.Email).IsRequired();
                entity.Property(e => e.Department).IsRequired();
                entity.Property(e => e.Salary).IsRequired().HasColumnType("decimal(18,2)");
            });

            modelBuilder.Entity<DotNetApiHangFire.Models.DailyEmployeeAudit>(entity =>
            {
                entity.ToTable("DailyEmployeeAudit");
                entity.HasKey(e => e.Id);

                entity.Property(e => e.AuditDate).IsRequired();
                entity.Property(e => e.TotalEmployees).IsRequired();
                entity.Property(e => e.TotalSalary).IsRequired().HasColumnType("decimal(18,2)");
            });
        }
    }
}
