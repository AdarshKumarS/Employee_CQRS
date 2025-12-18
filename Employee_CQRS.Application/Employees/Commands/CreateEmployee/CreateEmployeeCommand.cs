using MediatR;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Employee_CQRS.Application.Employees.Commands.CreateEmployee;

/// <summary>
/// Command represents an intention to create an employee.
/// It only contains data, no logic.
/// Why Command has no logic? - Command represents intention, logic should be inside handler to maintain SRP.
/// </summary>
public class CreateEmployeeCommand : IRequest<int>
{
    public string EmployeeName { get; set; } = string.Empty;
    public string MobileNo { get; set; } = string.Empty;
    public string EmailId { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string Pincode { get; set; } = string.Empty;
}
