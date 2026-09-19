using Microsoft.Extensions.Logging;

namespace DotNetApiHangFire.Services
{
    public class EmployeeNotificationService : IEmployeeNotificationService
    {
        private readonly ILogger<EmployeeNotificationService> _logger;

        public EmployeeNotificationService(ILogger<EmployeeNotificationService> logger)
        {
            _logger = logger;
        }

        public async Task EmployeeCreatedAsync(int employeeId)
        {
            _logger.LogInformation(
                "HANGFIRE JOB: Employee with ID {EmployeeId} was created.",
                employeeId);

            await Task.CompletedTask;
        }

        public async Task EmployeeDeletedAsync(int id)
        {
            _logger.LogInformation(
                "HANGFIRE JOB: Employee {EmployeeName} ({EmployeeEmail}) was deleted.",
                id);

            await Task.CompletedTask;
        }
    }
}
