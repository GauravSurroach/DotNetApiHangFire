namespace DotNetApiHangFire.Services
{
    public interface IEmployeeNotificationService
    {
        Task EmployeeCreatedAsync(int employeeId);
        Task EmployeeDeletedAsync(int employeeId);
    }
}
