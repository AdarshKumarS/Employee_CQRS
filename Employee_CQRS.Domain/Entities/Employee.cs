namespace Employee_CQRS.Domain.Entities;

/// <summary>
/// Domain entity representing Employee.
/// This class contains ONLY business data.
/// No EF Core, no validation, no logic.
/// </summary>
public class Employee
{
    public int Id { get; set; }
    public string EmployeeName { get; set; } = string.Empty;
    public string MobileNo { get; set; } = string.Empty;
    public string EmailId { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string Pincode { get; set; } = string.Empty;
}
