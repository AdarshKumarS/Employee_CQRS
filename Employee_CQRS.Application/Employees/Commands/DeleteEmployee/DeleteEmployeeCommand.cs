using MediatR;

namespace Employee_CQRS.Application.Employees.Commands.DeleteEmployee;

/// <summary>
/// Command to delete an employee.
/// </summary>
public class DeleteEmployeeCommand : IRequest<bool>
{
    public int Id { get; set; }
}
