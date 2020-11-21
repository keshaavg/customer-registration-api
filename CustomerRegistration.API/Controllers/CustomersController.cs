using AutoMapper;
using CustomerRegistration.API.Model;
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
        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor 
        /// </summary>
        /// <param name="repository"><see cref="ICustomerRepository"/> saves cutomer record to database</param>
        /// <param name="mapper"><see cref="IMapper"/> maps <see cref="CustomerDto"/> to <see cref="Customer"/> entity </param>
        public CustomersController(ICustomerRepository repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException("repository", "Customer Repository is not initialised"); ;
            _mapper = mapper ?? throw new ArgumentNullException("mapper", "Mapper is not initialised"); ;
        }

        /// <summary>
        /// This endpoint accepts and validates the <see cref="Customer"/> information for Registration
        /// If validation is successful it saves customer info in database and return a unique online Customer ID.
        /// This method uses Model validation with Fluent validation library (https://fluentvalidation.net/) and doesn't have 
        /// to check ModelState.IsValid as Controller has [ApiController] attribute. Therefore an automatic HTTP 400 response 
        /// containing error details is returned when model state is invalid.
        /// Success - 201 - Created
        /// Failure - 400- Bad Request
        /// </summary>
        /// <param name="customerDto"><see cref="Customer"/></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Customer>> RegisterCustomer(CustomerDto customerDto)
        {
            var customerId = await _repository.Add(_mapper.Map<Customer>(customerDto));

            return new CreatedResult("", new { customerId });
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
