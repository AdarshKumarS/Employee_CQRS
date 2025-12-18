using Employee_CQRS.Application.Common.Interfaces;
using Employee_CQRS.Application.Employees.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Employee_CQRS.Application.Employees.Queries.GetEmployeeById;

/// <summary>
/// Handles GetEmployeeByIdQuery.
/// </summary>
public class GetEmployeeByIdQueryHandler
    : IRequestHandler<GetEmployeeByIdQuery, EmployeeDto?>
{
    private readonly IApplicationDbContext _context;

    public GetEmployeeByIdQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<EmployeeDto?> Handle(
        GetEmployeeByIdQuery request,
        CancellationToken cancellationToken)
    {
        return await _context.Employees
            .AsNoTracking()
            .Where(e => e.Id == request.Id)
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
            .FirstOrDefaultAsync(cancellationToken);
    }
}
