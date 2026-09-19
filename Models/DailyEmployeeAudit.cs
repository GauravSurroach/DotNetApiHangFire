using System;

namespace DotNetApiHangFire.Models
{
    public class DailyEmployeeAudit
    {
        public int Id { get; set; }

        public DateTime AuditDate { get; set; }

        public int TotalEmployees { get; set; }

        public decimal TotalSalary { get; set; }
    }
}
