using Employee_CQRS.Application.Common.Interfaces;
using Employee_CQRS.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Employee_CQRS.Application.Tests.Common;

public class TestApplicationDbContext : DbContext, IApplicationDbContext
{
    public TestApplicationDbContext(DbContextOptions<TestApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Employee> Employees => Set<Employee>();

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        => base.SaveChangesAsync(cancellationToken);
}
