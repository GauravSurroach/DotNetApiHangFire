namespace DotNetApiHangFire.Services
{
    public interface IEmployeeAuditService
    {
        Task GenerateDailyAuditAsync();
    }
}
