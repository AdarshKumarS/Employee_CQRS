using MediatR;

namespace Employee_CQRS.Application.Employees.Commands.UpdateEmployee;

/// <summary>
/// Command to update an existing employee.
/// </summary>
public class UpdateEmployeeCommand : IRequest<bool>
{
    public int Id { get; set; }

    public string EmployeeName { get; set; } = string.Empty;
    public string MobileNo { get; set; } = string.Empty;
    public string EmailId { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string Pincode { get; set; } = string.Empty;
}
