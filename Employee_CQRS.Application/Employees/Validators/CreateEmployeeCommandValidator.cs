using Employee_CQRS.Application.Employees.Commands.CreateEmployee;
using FluentValidation;

namespace Employee_CQRS.Application.Employees.Validators;

/// <summary>
/// Validation rules for CreateEmployeeCommand.
/// </summary>
public class CreateEmployeeCommandValidator
    : AbstractValidator<CreateEmployeeCommand>
{
    public CreateEmployeeCommandValidator()
    {
        RuleFor(x => x.EmployeeName)
            .NotEmpty().WithMessage("Employee name is required")
            .MaximumLength(100);

        RuleFor(x => x.MobileNo)
            .NotEmpty()
            .Matches("^[0-9]{10}$")
            .WithMessage("Mobile number must be 10 digits");

        RuleFor(x => x.EmailId)
            .NotEmpty()
            .EmailAddress();

        RuleFor(x => x.State)
            .NotEmpty();

        RuleFor(x => x.City)
            .NotEmpty();

        RuleFor(x => x.Pincode)
            .NotEmpty()
            .Matches("^[0-9]{6}$")
            .WithMessage("Pincode must be 6 digits");
    }
}
