using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DotNetApiHangFire.Data;
using DotNetApiHangFire.Models;
using Hangfire;
using DotNetApiHangFire.Services;

namespace DotNetApiHangFire.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly IEmployeeNotificationService _notificationService;

        public EmployeeController(ApplicationDbContext db,
            IEmployeeNotificationService notificationService)
        {
            _db = db;
            _notificationService = notificationService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var employees = await _db.Employees.ToListAsync();
            return Ok(employees);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var employee = await _db.Employees.FirstOrDefaultAsync(e => e.Id == id);
            if (employee == null) return NotFound();
            return Ok(employee);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Employees employee)
        {
            if (employee == null) return BadRequest();

            _db.Employees.Add(employee);
            await _db.SaveChangesAsync();

            // Trigger notification
            BackgroundJob.Enqueue<IEmployeeNotificationService>(
                          service => service.EmployeeCreatedAsync(employee.Id));

            return CreatedAtAction(nameof(GetById), new { id = employee.Id }, employee);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] Employees updated)
        {
            if (updated == null) return BadRequest();

            var employee = await _db.Employees.FindAsync(id);
            if (employee == null) return NotFound();

            employee.Name = updated.Name;
            employee.Email = updated.Email;
            employee.Department = updated.Department;
            employee.Salary = updated.Salary;

            await _db.SaveChangesAsync();

            return Ok(employee);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var employee = await _db.Employees.FindAsync(id);
            if (employee == null) return NotFound();

            _db.Employees.Remove(employee);
            await _db.SaveChangesAsync();

            BackgroundJob.Enqueue<IEmployeeNotificationService>(
        service => service.EmployeeDeletedAsync(id));

            return NoContent();
        }
    }
}
