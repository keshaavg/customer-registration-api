using AutoMapper;
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
            var mockMapper = new Mock<IMapper>();
            mockRepo.Setup(repo => repo.Add(It.IsAny<Customer>())).ReturnsAsync(1);
            mockMapper.Setup(mapper => mapper.Map<Customer>(It.IsAny<CustomerDto>())).Returns(new Customer());
            var controller = new CustomersController(mockRepo.Object, mockMapper.Object);
            var customer = new CustomerDto();

            // Act
            var result = await controller.RegisterCustomer(customer);

            // Assert
            var createdResult = result.Result as CreatedResult;

            // assert
            Assert.NotNull(createdResult);
            Assert.Equal(201, createdResult.StatusCode);
        }
    }
}
