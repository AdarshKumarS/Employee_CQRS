using Employee_CQRS.Application.Employees.DTOs;
using MediatR;

namespace Employee_CQRS.Application.Employees.Queries.GetEmployees;

/// <summary>
/// Query to get all employees.
/// </summary>
public record GetEmployeesQuery : IRequest<List<EmployeeDto>>;
