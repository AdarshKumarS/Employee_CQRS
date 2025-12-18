using Employee_CQRS.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Employee_CQRS.Application.Employees.Commands.DeleteEmployee;

/// <summary>
/// Handles deleting employee.
/// </summary>
public class DeleteEmployeeCommandHandler
    : IRequestHandler<DeleteEmployeeCommand, bool>
{
    private readonly IApplicationDbContext _context;
    private readonly ILogger<DeleteEmployeeCommandHandler> _logger;

    public DeleteEmployeeCommandHandler(
        IApplicationDbContext context,
        ILogger<DeleteEmployeeCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> Handle(
        DeleteEmployeeCommand request,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Attempting to delete employee with Id {EmployeeId}",
            request.Id);

        var employee = await _context.Employees
            .FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken);

        if (employee == null)
        {
            _logger.LogWarning(
                "Delete failed. Employee with Id {EmployeeId} not found",
                request.Id);

            return false;
        }

        _context.Employees.Remove(employee);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Employee with Id {EmployeeId} deleted successfully",
            request.Id);

        return true;
    }
}
