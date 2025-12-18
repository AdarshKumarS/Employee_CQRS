using Employee_CQRS.Application.Common.Interfaces;
using Employee_CQRS.Application.Employees.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Employee_CQRS.Application.Employees.Queries.GetEmployees;

/// <summary>
/// Handles GetEmployeesQuery.
/// Why AsNoTracking()? - Improves performance for read-only queries.
/// </summary>
public class GetEmployeesQueryHandler
    : IRequestHandler<GetEmployeesQuery, List<EmployeeDto>>
{
    private readonly IApplicationDbContext _context;

    public GetEmployeesQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<EmployeeDto>> Handle(
        GetEmployeesQuery request,
        CancellationToken cancellationToken)
    {
        return await _context.Employees
            .AsNoTracking()
            .Select(e => new EmployeeDto
            {
                Id = e.Id,
                EmployeeName = e.EmployeeName,
                MobileNo = e.MobileNo,
                EmailId = e.EmailId,
                State = e.State,
                City = e.City,
                Pincode = e.Pincode
            })
            .ToListAsync(cancellationToken);
    }
}
