using Employee_CQRS.API.Models;
using Employee_CQRS.Application.Employees.Commands.CreateEmployee;
using Employee_CQRS.Application.Employees.Commands.DeleteEmployee;
using Employee_CQRS.Application.Employees.Commands.UpdateEmployee;
using Employee_CQRS.Application.Employees.Queries.GetEmployeeById;
using Employee_CQRS.Application.Employees.Queries.GetEmployees;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using static System.Net.WebRequestMethods;

namespace Employee_CQRS.API.Controllers;

//Why controller is thin?
//Controller should only handle HTTP concerns.
//Business logic stays in Application layer via CQRS.

[ApiController]
[Route("api/[controller]")]
public class EmployeesController : ControllerBase
{
    private readonly IMediator _mediator;

    public EmployeesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Creates a new employee.
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Create(CreateEmployeeCommand command)
    {
        var employeeId = await _mediator.Send(command);
        return Ok(ApiResponse<string>.SuccessResponse(
        "Success",
        "Employee created successfully"
        ));
    }

    /// <summary>
    /// Get All Employees.
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var data = await _mediator.Send(new GetEmployeesQuery());

        return Ok(ApiResponse<object>.SuccessResponse(data));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(
    int id,
    UpdateEmployeeCommand command)
    {
        if (id != command.Id)
            return BadRequest("Id mismatch");

        var updated = await _mediator.Send(command);

        if (!updated)
            return NotFound($"Employee with Id {id} not found");

        return NoContent();
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _mediator.Send(new DeleteEmployeeCommand { Id = id });

        if (!deleted)
            return NotFound($"Employee with Id {id} not found");

        return NoContent();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var employee = await _mediator.Send(
            new GetEmployeeByIdQuery { Id = id });

        if (employee == null)
            throw new KeyNotFoundException($"Employee {id} not found");

        return Ok(ApiResponse<object>.SuccessResponse(employee));
    }

}
