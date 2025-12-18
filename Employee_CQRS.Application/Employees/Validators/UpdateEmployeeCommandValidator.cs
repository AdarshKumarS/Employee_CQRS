using Employee_CQRS.Application.Employees.Commands.UpdateEmployee;
using FluentValidation;

namespace Employee_CQRS.Application.Employees.Validators;

/// <summary>
/// Validation rules for UpdateEmployeeCommand.
/// </summary>
public class UpdateEmployeeCommandValidator
    : AbstractValidator<UpdateEmployeeCommand>
{
    public UpdateEmployeeCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0);

        RuleFor(x => x.EmployeeName)
            .NotEmpty();

        RuleFor(x => x.MobileNo)
            .Matches("^[0-9]{10}$");

        RuleFor(x => x.EmailId)
            .EmailAddress();

        RuleFor(x => x.Pincode)
            .Matches("^[0-9]{6}$");
    }
}
