using Employee_CQRS.Application.Employees.Commands.CreateEmployee;
using Employee_CQRS.Application.Tests.Common;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Employee_CQRS.Application.Tests.Employees.Commands
{
    public class CreateEmployeeCommandHandlerTests
    {
        [Fact]
        public async Task Handle_Should_Create_Employee_And_Return_Id()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<TestApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "CreateEmployeeDb")
                .Options;

            var context = new TestApplicationDbContext(options);

            var loggerMock = new Mock<ILogger<CreateEmployeeCommandHandler>>();

            var handler = new CreateEmployeeCommandHandler(
                context,
                loggerMock.Object
            );

            var command = new CreateEmployeeCommand
            {
                EmployeeName = "Adarsh Kumar",
                MobileNo = "9876543210",
                EmailId = "adarsh@gmail.com",
                State = "Karnataka",
                City = "Bangalore",
                Pincode = "560001"
            };

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().BeGreaterThan(0);

            var employee = await context.Employees.FirstOrDefaultAsync();
            employee.Should().NotBeNull();
            employee!.EmployeeName.Should().Be("Adarsh Kumar");
        }
    }
}