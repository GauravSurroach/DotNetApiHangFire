using Microsoft.EntityFrameworkCore;
using DotNetApiHangFire.Data;
using Hangfire;
using Hangfire.SqlServer;
using DotNetApiHangFire.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure DbContext with SQL Server
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

// Configure Hangfire with SQL Server storage
builder.Services.AddHangfire(config =>
    config
        .UseSimpleAssemblyNameTypeSerializer()
        .UseRecommendedSerializerSettings()
        .UseSqlServerStorage(
            builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddHangfireServer();
builder.Services.AddScoped<IEmployeeNotificationService, EmployeeNotificationService>();
builder.Services.AddScoped<IEmployeeAuditService, EmployeeAuditService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseHangfireDashboard("/hangfire");

// Register recurring Hangfire job to run every day at 9:00 AM
RecurringJob.AddOrUpdate<IEmployeeAuditService>(
    "daily-employee-audit",
    service => service.GenerateDailyAuditAsync(),
    Cron.Daily(9));

app.MapControllers();

app.Run();
