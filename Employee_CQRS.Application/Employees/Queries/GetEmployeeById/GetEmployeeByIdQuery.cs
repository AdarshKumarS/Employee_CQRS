using Employee_CQRS.Application.Employees.DTOs;
using MediatR;

namespace Employee_CQRS.Application.Employees.Queries.GetEmployeeById;

/// <summary>
/// Query to get employee by Id.
/// </summary>
public class GetEmployeeByIdQuery : IRequest<EmployeeDto?>
{
    public int Id { get; set; }
}
