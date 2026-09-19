using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using DotNetApiHangFire.Data;
using DotNetApiHangFire.Models;

namespace DotNetApiHangFire.Services
{
    public class EmployeeAuditService : IEmployeeAuditService
    {
        private readonly ApplicationDbContext _db;
        private readonly ILogger<EmployeeAuditService> _logger;

        public EmployeeAuditService(ApplicationDbContext db, ILogger<EmployeeAuditService> logger)
        {
            _db = db;
            _logger = logger;
        }

        public async Task GenerateDailyAuditAsync()
        {
            try
            {
                // Table already exists in the database. Read current employees and insert audit record.
                var totalEmployees = await _db.Employees.CountAsync();
                var totalSalary = await _db.Employees.SumAsync(e => (decimal?)e.Salary) ?? 0m;

                var audit = new DailyEmployeeAudit
                {
                    AuditDate = DateTime.Now,
                    TotalEmployees = totalEmployees,
                    TotalSalary = totalSalary
                };

                _db.DailyEmployeeAudits.Add(audit);
                await _db.SaveChangesAsync();

                _logger.LogInformation("Daily employee audit created: {AuditId} (Employees={TotalEmployees}, TotalSalary={TotalSalary})", audit.Id, audit.TotalEmployees, audit.TotalSalary);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error generating daily employee audit");
                throw;
            }
        }
    }
}
