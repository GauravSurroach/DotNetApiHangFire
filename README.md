# DotNetApiHangFire
# .NET 8 Web API with Hangfire

A practical **ASP.NET Core .NET 8 Web API** demonstrating Employee CRUD operations, Entity Framework Core, SQL Server, and **Hangfire background job processing**.

### Key Features

* ASP.NET Core .NET 8 Web API
* Employee CRUD operations
* Entity Framework Core with SQL Server
* Hangfire with SQL Server storage
* Hangfire Dashboard for monitoring jobs
* Fire-and-forget background jobs using `BackgroundJob.Enqueue()`
* Recurring background jobs using `RecurringJob.AddOrUpdate()`
* Daily automated employee audit at 9:00 AM
* Daily audit stores employee count and total salary
* Dependency Injection and service-based architecture
* Async/await with Entity Framework Core

### Hangfire Examples

**Fire-and-forget job**

When an employee is created or deleted, a Hangfire background job is triggered for post-processing.

**Recurring job**

A recurring Hangfire job runs every day at 9:00 AM and generates an employee audit containing:

* Total employees
* Total salary
* Audit date/time

### Technologies

`C#` · `.NET 8` · `ASP.NET Core Web API` · `Entity Framework Core` · `SQL Server` · `Hangfire` · `REST API` · `Dependency Injection`
