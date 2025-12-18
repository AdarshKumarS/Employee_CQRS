using Employee_CQRS.Application.Employees.Queries.GetEmployeeById;
using Employee_CQRS.Application.Tests.Common;
using Employee_CQRS.Domain.Entities;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Employee_CQRS.Application.Tests.Employees.Queries
{
    public class GetEmployeeByIdQueryHandlerTests
    {
        [Fact]
        public async Task Handle_Should_Return_Employee_When_Exists()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<TestApplicationDbContext>()
                .UseInMemoryDatabase("GetEmployeeByIdDb")
                .Options;

            var context = new TestApplicationDbContext(options);

            context.Employees.Add(new Employee
            {
                Id = 1,
                EmployeeName = "Adarsh",
                MobileNo = "9999999999",
                EmailId = "adarsh@gmail.com",
                State = "KA",
                City = "BLR",
                Pincode = "560001"
            });

            await context.SaveChangesAsync(CancellationToken.None);

            var handler = new GetEmployeeByIdQueryHandler(context);

            // Act
            var result = await handler.Handle(
                new GetEmployeeByIdQuery { Id = 1 },
                CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result!.EmployeeName.Should().Be("Adarsh");
            result.EmailId.Should().Be("adarsh@gmail.com");
        }

        [Fact]
        public async Task Handle_Should_Return_Null_When_Not_Found()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<TestApplicationDbContext>()
                .UseInMemoryDatabase("GetEmployeeByIdNotFoundDb")
                .Options;

            var context = new TestApplicationDbContext(options);

            var handler = new GetEmployeeByIdQueryHandler(context);

            // Act
            var result = await handler.Handle(
                new GetEmployeeByIdQuery { Id = 99 },
                CancellationToken.None);

            // Assert
            result.Should().BeNull();
        }
    }
}
