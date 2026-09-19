namespace DotNetApiHangFire.Models
{
    public class Employees
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Department { get; set; } = null!;
        public decimal Salary { get; set; }
    }
}
