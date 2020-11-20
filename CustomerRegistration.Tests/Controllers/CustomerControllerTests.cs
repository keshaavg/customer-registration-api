using CustomerRegistration.API;
using CustomerRegistration.API.Controllers;
using CustomerRegistration.API.Repository;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace CustomerRegistration.Tests.Controllers
{
    /// <summary>
    /// Test class to test <see cref="CustomersController"/> class
    /// </summary>
    public class CustomerControllerTests
    {
        [Fact]
        public async Task RegsiterCustomer_CreatedResult_WithSuccessSaveToDatabase()
        {
            // Arrange
            var mockRepo = new Mock<ICustomerRepository>();
            mockRepo.Setup(repo => repo.Add(It.IsAny<Customer>())).ReturnsAsync(1);
            var controller = new CustomersController(mockRepo.Object);
            var customer = new Customer();

            // Act
            var result = await controller.RegisterCustomer(customer);

            // Assert
            var createdResult = result.Result as CreatedResult;

            // assert
            Assert.NotNull(createdResult);
            Assert.Equal(201, createdResult.StatusCode);
        }

        [Fact]
        public async Task RegisterCustomer_ConflictResult_WithFailureToSaveToDatabase()
        {
            // Arrange
            var mockRepo = new Mock<ICustomerRepository>();
            mockRepo.Setup(repo => repo.Add(It.IsAny<Customer>())).ReturnsAsync(0);
            var controller = new CustomersController(mockRepo.Object);
            var customer = new Customer();

            // Act
            var result = await controller.RegisterCustomer(customer);

            // Assert
            var conflictResult = result.Result as ConflictObjectResult;

            // assert
            Assert.NotNull(conflictResult);
            Assert.Equal(409, conflictResult.StatusCode);
        }
    }
}
