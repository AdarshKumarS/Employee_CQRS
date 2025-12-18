using Employee_CQRS.Application.Common.Interfaces;
using Employee_CQRS.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Employee_CQRS.Infrastructure.Persistence;

/// <summary>
/// EF Core DbContext.
/// This is the ONLY place where EF Core is implemented.
/// Because Infrastructure contains all external dependencies like database, EF Core, file system, etc.
/// </summary>
public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Employee> Employees => Set<Employee>();
}
