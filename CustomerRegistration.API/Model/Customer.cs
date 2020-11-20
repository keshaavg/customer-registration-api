using System;

namespace CustomerRegistration.API
{
    /// <summary>
    /// Class to hold customer information.
    /// </summary>
    public class Customer
    {
        /// <summary>
        /// Gets or sets unique <see cref="Customer"/> id
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        /// Gets or sets First name of <see cref="Customer"/> 
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets Last name of <see cref="Customer"/> 
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets Policy Reference Number of <see cref="Customer"/> 
        /// </summary>
        public string PolicyReferenceNumber { get; set; }

        /// <summary>
        /// Gets or sets Date Of Birth of <see cref="Customer"/> 
        /// </summary>
        public DateTime DateOfBirth { get; set; }

        /// <summary>
        /// Gets or sets Email of <see cref="Customer"/> 
        /// </summary>
        public string Email { get; set; }
    }
}
