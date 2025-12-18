using Employee_CQRS.Application.Employees.Queries.GetEmployees;
using Employee_CQRS.Application.Tests.Common;
using Employee_CQRS.Domain.Entities;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;


namespace Employee_CQRS.Application.Tests.Employees.Queries
{
    public class GetEmployeesQueryHandlerTests
    {
        [Fact]
        public async Task Handle_Should_Return_All_Employees()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<TestApplicationDbContext>()
                .UseInMemoryDatabase("GetEmployeesDb")
                .Options;

            var context = new TestApplicationDbContext(options);

            context.Employees.AddRange(
                new Employee
                {
                    Id = 1,
                    EmployeeName = "Emp 1",
                    MobileNo = "1111111111",
                    EmailId = "emp1@gmail.com",
                    State = "KA",
                    City = "BLR",
                    Pincode = "560001"
                },
                new Employee
                {
                    Id = 2,
                    EmployeeName = "Emp 2",
                    MobileNo = "2222222222",
                    EmailId = "emp2@gmail.com",
                    State = "KA",
                    City = "BLR",
                    Pincode = "560001"
                }
            );

            await context.SaveChangesAsync(CancellationToken.None);

            var handler = new GetEmployeesQueryHandler(context);

            // Act
            var result = await handler.Handle(
                new GetEmployeesQuery(),
                CancellationToken.None);

            // Assert
            result.Should().HaveCount(2);
            result.Select(x => x.EmployeeName)
                  .Should().Contain(new[] { "Emp 1", "Emp 2" });
        }
    }

}
