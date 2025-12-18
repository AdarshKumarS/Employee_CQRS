using Employee_CQRS.Application.Common.Interfaces;
using Employee_CQRS.Application.Employees.Commands.CreateEmployee;
using Employee_CQRS.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

public class CreateEmployeeCommandHandler
    : IRequestHandler<CreateEmployeeCommand, int>
{
    private readonly IApplicationDbContext _context;
    private readonly ILogger<CreateEmployeeCommandHandler> _logger;

    public CreateEmployeeCommandHandler(
        IApplicationDbContext context,
        ILogger<CreateEmployeeCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<int> Handle(
        CreateEmployeeCommand request,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Creating employee with email {Email}",
            request.EmailId);

        var employee = new Employee
        {
            EmployeeName = request.EmployeeName,
            MobileNo = request.MobileNo,
            EmailId = request.EmailId,
            State = request.State,
            City = request.City,
            Pincode = request.Pincode
        };

        _context.Employees.Add(employee);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Employee created successfully with Id {EmployeeId}",
            employee.Id);

        return employee.Id;
    }
}
