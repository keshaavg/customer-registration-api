using CustomerRegistration.API.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CustomerRegistration.API.Repository
{
    /// <summary>
    /// <see cref="CustomerRepository"/> which saves customers data using <see cref="CustomerContext"/>
    /// and handles db errors
    /// </summary>
    public class CustomerRepository : ICustomerRepository
    {
        private readonly CustomerContext _context;

        /// <summary>
        /// Constructor 
        /// </summary>
        /// <param name="context"></param>
        public CustomerRepository(CustomerContext context)
        {
            _context = context ?? throw new ArgumentNullException("context", "DB Context is not initialised"); ;
        }

        /// <summary>
        /// Add customer to database if customer with same id exist in database raise error. 
        /// </summary>
        /// <param name="customer"><see cref="Customer"/></param>
        /// <returns>If success returs newly assigned customer id else 0</returns>
        public async Task<int> Add(Customer customer)
        {
           _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
            return customer.CustomerId;
        }

        /// <summary>
        /// Gets all the customer from database for test purpose only
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Customer>> GetAll()
        {
            return await _context.Customers.ToListAsync();
        }
    }
}
