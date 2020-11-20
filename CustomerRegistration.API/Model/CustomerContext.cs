using Microsoft.EntityFrameworkCore;

namespace CustomerRegistration.API.Model
{

    /// <summary>
    /// This class exposes the generic <see cref="DbSet<Customer>"/>. 
    /// </summary>
    public class CustomerContext : DbContext
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="options"><see cref="DbContextOptions"/></param>
        public CustomerContext(DbContextOptions<CustomerContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// DbSet provides the methods to manage the <see cref="Customer"/>entity set. 
        /// </summary>
        public DbSet<Customer> Customers { get; set; }
    }
}
