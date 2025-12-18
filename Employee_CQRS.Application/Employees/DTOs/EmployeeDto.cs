namespace Employee_CQRS.Application.Employees.DTOs;

/// <summary>
/// Data Transfer Object for Employee.
/// Used for READ operations.
/// Why DTO instead of Entity? - Prevents over-exposing domain models and avoids coupling API with DB schema.
/// </summary>
public class EmployeeDto
{
    public int Id { get; set; }
    public string EmployeeName { get; set; } = string.Empty;
    public string MobileNo { get; set; } = string.Empty;
    public string EmailId { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string Pincode { get; set; } = string.Empty;
}
