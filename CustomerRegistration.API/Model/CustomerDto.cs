using System;

namespace CustomerRegistration.API
{
    /// <summary>
    /// Data transfer object for <see cref="Customer"/> entity. Separate model to ensure
    /// customer id cannot be passed by client and unique id is always generated by database
    /// </summary>
    public class CustomerDto
    {
        /// <summary>
        /// Gets or sets First name of <see cref="CustomerDto"/> 
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets Last name of <see cref="CustomerDto"/> 
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets Policy Reference Number of <see cref="CustomerDto"/> 
        /// </summary>
        public string PolicyReferenceNumber { get; set; }

        /// <summary>
        /// Gets or sets Date Of Birth of <see cref="CustomerDto"/> 
        /// </summary>
        public DateTime DateOfBirth { get; set; }

        /// <summary>
        /// Gets or sets Email of <see cref="CustomerDto"/> 
        /// </summary>
        public string Email { get; set; }
    }
}
