using Employee_CQRS.Application.Employees.Commands.DeleteEmployee;
using Employee_CQRS.Application.Tests.Common;
using Employee_CQRS.Domain.Entities;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Employee_CQRS.Application.Tests.Employees.Commands 
{ 
    public class DeleteEmployeeCommandHandlerTests
    {
        [Fact]
        public async Task Handle_Should_Delete_Employee_When_Exists()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<TestApplicationDbContext>()
                .UseInMemoryDatabase("DeleteEmployeeDb")
                .Options;

            var context = new TestApplicationDbContext(options);

            context.Employees.Add(new Employee
            {
                Id = 1,
                EmployeeName = "Test",
                MobileNo = "9999999999",
                EmailId = "test@gmail.com",
                State = "KA",
                City = "BLR",
                Pincode = "560001"
            });
            await context.SaveChangesAsync(CancellationToken.None);

            var loggerMock = new Mock<ILogger<DeleteEmployeeCommandHandler>>();
            var handler = new DeleteEmployeeCommandHandler(context, loggerMock.Object);

            // Act
            var result = await handler.Handle(
                new DeleteEmployeeCommand { Id = 1 },
                CancellationToken.None);

            // Assert
            result.Should().BeTrue();
            context.Employees.Should().BeEmpty();
        }
    }
}