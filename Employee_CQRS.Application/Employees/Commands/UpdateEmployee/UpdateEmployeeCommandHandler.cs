using Employee_CQRS.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Employee_CQRS.Application.Employees.Commands.UpdateEmployee;

/// <summary>
/// Handles updating employee.
/// </summary>
public class UpdateEmployeeCommandHandler
    : IRequestHandler<UpdateEmployeeCommand, bool>
{
    private readonly IApplicationDbContext _context;
    private readonly ILogger<UpdateEmployeeCommandHandler> _logger;

    public UpdateEmployeeCommandHandler(
        IApplicationDbContext context,
        ILogger<UpdateEmployeeCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> Handle(
        UpdateEmployeeCommand request,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Updating employee with Id {EmployeeId}",
            request.Id);

        var employee = await _context.Employees
            .FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken);

        if (employee == null)
        {
            _logger.LogWarning(
                "Update failed. Employee with Id {EmployeeId} not found",
                request.Id);

            return false;
        }

        employee.EmployeeName = request.EmployeeName;
        employee.MobileNo = request.MobileNo;
        employee.EmailId = request.EmailId;
        employee.State = request.State;
        employee.City = request.City;
        employee.Pincode = request.Pincode;

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Employee with Id {EmployeeId} updated successfully",
            request.Id);

        return true;
    }
}
