using Employee_CQRS.Application.Employees.Commands.UpdateEmployee;
using Employee_CQRS.Application.Tests.Common;
using Employee_CQRS.Domain.Entities;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Employee_CQRS.Application.Tests.Employees.Commands 
{
    public class UpdateEmployeeCommandHandlerTests
    {
        [Fact]
        public async Task Handle_Should_Update_Employee_When_Exists()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<TestApplicationDbContext>()
                .UseInMemoryDatabase("UpdateEmployeeDb")
                .Options;

            var context = new TestApplicationDbContext(options);

            context.Employees.Add(new Employee
            {
                Id = 1,
                EmployeeName = "Old Name",
                MobileNo = "9999999999",
                EmailId = "old@gmail.com",
                State = "KA",
                City = "BLR",
                Pincode = "560001"
            });
            await context.SaveChangesAsync(CancellationToken.None);

            var loggerMock = new Mock<ILogger<UpdateEmployeeCommandHandler>>();
            var handler = new UpdateEmployeeCommandHandler(context, loggerMock.Object);

            var command = new UpdateEmployeeCommand
            {
                Id = 1,
                EmployeeName = "New Name",
                MobileNo = "8888888888",
                EmailId = "new@gmail.com",
                State = "KA",
                City = "BLR",
                Pincode = "560001"
            };

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().BeTrue();
            context.Employees.First().EmployeeName.Should().Be("New Name");
        }
    }
}
