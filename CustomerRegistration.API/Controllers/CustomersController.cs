﻿using CustomerRegistration.API.Model;
using CustomerRegistration.API.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CustomerRegistration.API.Controllers
{
    /// <summary>
    /// Main controller class provides endpoint to allow customers to register for the AFI customer portal.
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerRepository _repository;

        /// <summary>
        /// Constructor, Injects <see cref="CustomerContext"/>
        /// </summary>
        /// <param name="context"></param>
        public CustomersController(ICustomerRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException("repository", "Customer Repository is not initialised"); ;
        }

        /// <summary>
        /// This endpoint accepts and validates the <see cref="Customer"/> information for Registration
        /// If validation is successful it saves customer info in database and return a unique online Customer ID.
        /// This method uses Model validation with Fluent validation library (https://fluentvalidation.net/) and doesn't have 
        /// to check ModelState.IsValid as Controller has [ApiController] attribute. Therefore an automatic HTTP 400 response 
        /// containing error details is returned when model state is invalid.
        /// Success - 201 - Created
        /// Failure - 400- Bad Request
        /// Duplicate - 409- Conflict
        /// </summary>
        /// <param name="customer"><see cref="Customer"/></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Customer>> RegisterCustomer(Customer customer)
        {
            var customerId = await _repository.Add(customer);

            if (customerId == 0)
            {
                return new ConflictObjectResult($"Customer alredy exist with id {customer.CustomerId}");
            }
            else
            {
                return new CreatedResult("", customer);
            }
        }

        /// <summary>
        /// Added as helper method to get list of all Customer stored in customer database.
        /// </summary>
        /// <returns><see cref="IEnumerable<Customer>"/></returns>
        [HttpGet]
        public ActionResult<Customer> GetAll()
        {
            return new OkObjectResult(_repository.GetAll());
        }
    }
}