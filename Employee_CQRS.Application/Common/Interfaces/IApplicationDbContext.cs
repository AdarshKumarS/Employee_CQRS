using Employee_CQRS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Employee_CQRS.Application.Common.Interfaces;

/// <summary>
/// Abstraction over DbContext.
/// Application layer depends on this interface, not EF Core.
/// </summary>
public interface IApplicationDbContext
{
    DbSet<Employee> Employees { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
