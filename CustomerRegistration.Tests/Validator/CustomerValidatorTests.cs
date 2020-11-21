using CustomerRegistration.API;
using CustomerRegistration.API.Validators;
using Microsoft.Extensions.Options;
using System;
using Xunit;

namespace CustomerRegistration.Tests
{
    /// <summary>
    /// Test class to test <see cref="CustomerValidator"/> class
    /// </summary>
    public class CustomerValidatorTests
    {
        private readonly CustomerDto _customer;
        private readonly CustomerValidator _customerValidator;

        /// <summary>
        /// Initailises valid customer object to be used in all tests. Also defines <see cref="ValidatorConfig"/>
        /// </summary>
        public CustomerValidatorTests()
        {
            this._customer = new CustomerDto()
            {
                FirstName = "John",
                LastName = "Test",
                PolicyReferenceNumber = "AA-000001",
                Email = "abcd@a1.co.uk",
                DateOfBirth = DateTime.Now.AddYears(-20)
            };

            // Set up mock config to inject in Cstomer Validator
            ValidatorConfig config = new ValidatorConfig()
            {

                MinimumCustomerAge = 18,
                MinimumNameLength = 3,
                MaximumNameLength = 50,
                PolicyReferencePattern = @"^[A-Z]{2}-\d{6}$",
                EmailPattern = @"^[\w]{4,}@\w{2,}(.com|.co.uk)$"
            };

            IOptions<ValidatorConfig> options = Options.Create(config);

            this._customerValidator = new CustomerValidator(options);
        }

        /// <summary>
        /// Test Customer object must be Valid if First name, Last name, Policy Reference number and 
        /// Email is provided.Policy Reference number and email is corretly formatted
        /// </summary>
        [Fact]
        public void TestCustomerObjectIsValidForExpectedPropertyValuesWithEmptyDateOFBirthButValidEmail()
        {

            this._customer.DateOfBirth = default;
            var results = _customerValidator.Validate(this._customer);

            Assert.True(results.IsValid);
        }

        /// <summary>
        /// Test Customer object must be Valid if First name, Last name, Policy Reference number andDate Of Birth
        /// is provided. Policy Reference number correcctly formatted and age is 18 or more
        /// </summary>
        [Fact]
        public void TestCustomerObjectIsValidForExpectedPropertyValuesWithEmptyEmailButValidDateOfBirth()
        {
            this._customer.Email = null;
            var results = _customerValidator.Validate(this._customer);

            Assert.True(results.IsValid);
        }


        /// <summary>
        /// Test Customer object must be InValid if every value is null and error count will be 5.
        /// </summary>
        [Fact]
        public void TestCustomerObjectIsInValidForAllTheValuesSetToNull()
        {
            var customer = new CustomerDto();
            var results = _customerValidator.Validate(customer);

            Assert.False(results.IsValid);
            Assert.Equal(5, results.Errors.Count);
        }

        /// <summary>
        /// Test Customer object must be Invalid if First name is null, blank, whitespaces
        /// or length is less than min defined and more than max defined in config.
        /// </summary>
        /// <param name="firstName"></param>
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("    ")]
        [InlineData("aa")]
        [InlineData("My First name  is more than fifty charcters long. Please enter shorter name")]
        public void TestCustomerObjectIsInvalidForInvalidFirstName(string firstName)
        {

            this._customer.FirstName = firstName;
            var results = _customerValidator.Validate(this._customer);
            Assert.False(results.IsValid);
        }

        /// <summary>
        /// Test Customer object must be Invalid if Last name is null, blank, whitespaces 
        /// or length is less than min defined and more than max defined in config.
        /// </summary>
        /// <param name="lastName"></param>
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("    ")]
        [InlineData("aa")]
        [InlineData("My Last name  is more than fifty charcters long. Please enter shorter name")]
        public void TestCustomerObjectIsInvalidForInvalidLastName(string lastName)
        {

            this._customer.LastName = lastName;
            var results = _customerValidator.Validate(this._customer);
            Assert.False(results.IsValid);
        }

        /// <summary>
        /// Test Customer object must be Invalid if Policy Reference number is 
        /// null, blank, whitespaces or does not match pattern `AA-9999`
        /// </summary>
        /// <param name="policyReferenceNumber"></param>
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("    ")]
        [InlineData("AAA-000000")] // Not Exactly 2 letter before -
        [InlineData("A-000000")] // Only 1 letter before -
        [InlineData("AA-0000001")] // Not Exactly 6 digits after -
        [InlineData("AA-00000")] // Only 5 digits after -
        [InlineData("AA000000")] // No - seperater
        [InlineData("aa-000000")] // Small caps  letters before -
        public void TestCustomerObjectIsInvalidForInvalidPolicyReferenceNumber(string policyReferenceNumber)
        {

            this._customer.PolicyReferenceNumber = policyReferenceNumber;
            var results = _customerValidator.Validate(this._customer);
            Assert.False(results.IsValid);
        }

        /// <summary>
        /// Test Customer object must be Invalid if customer age is less than 18 years
        /// </summary>
        /// <param name="firstName"></param>
        [Fact]
        public void TestCustomerObjectIsInvalidIfCustomerAgeIsLessThan18Years()
        {

            this._customer.DateOfBirth = DateTime.Now.AddYears(-15);
            var results = _customerValidator.Validate(this._customer);
            Assert.False(results.IsValid);
        }

        /// <summary>
        /// Test Customer object must be Invalid if customer age is less than 18 years by a day
        /// </summary>
        /// <param name="firstName"></param>
        [Fact]
        public void TestCustomerObjectIsInvalidIfCustomerAgeIs18YearsAndaDayLess()
        {
            this._customer.DateOfBirth = DateTime.Now.AddYears(-18).AddDays(1);
            var results = _customerValidator.Validate(this._customer);
            Assert.False(results.IsValid);
        }

        /// <summary>
        /// Test Customer object must be Valid if customer age is exactly 18 years
        /// </summary>
        /// <param name="firstName"></param>
        [Fact]
        public void TestCustomerObjectIsValidIfCustomerAge18Years()
        {

            this._customer.DateOfBirth = DateTime.Now.AddYears(-18);
            var results = _customerValidator.Validate(this._customer);
            Assert.True(results.IsValid);
        }

        /// <summary>
        /// Test Customer object must be Valid if customer age is 18 years and a day more
        /// </summary>
        /// <param name="firstName"></param>
        [Fact]
        public void TestCustomerObjectIsValidIfCustomerAge18YearsAndADayMore()
        {

            this._customer.DateOfBirth = DateTime.Now.AddYears(-18).AddDays(-1);
            var results = _customerValidator.Validate(this._customer);
            Assert.True(results.IsValid);
        }

        /// <summary>
        /// Test Customer object must be Valid if customer date of Birth is empty but correct 
        /// formatted email is provided
        /// </summary>
        /// <param name="firstName"></param>
        [Fact]
        public void TestCustomerObjectIsValidIfCustomerDateOfBirthIsEmptyForValidEmail()
        {
            this._customer.DateOfBirth = default;
            var results = _customerValidator.Validate(this._customer);
            Assert.True(results.IsValid);
        }

        /// <summary>
        /// Test Customer object must be Invalid if date of birth is not provided but email
        /// is null, empty, whitespace or does not match the pattern. 
        /// Valid Email Format Regex: "^[\w]{4,}@\w{2,}(.com|.co.uk)$"
        /// Email address starts with least 4 alpha numeric chars followed by an ‘@’ sign and 
        /// then another string of at least 2 alpha numeric chars. Ends in either ‘.com’ or ‘.co.uk’.
        /// </summary>
        /// <param name="firstName"></param>
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("    ")]
        [InlineData("abc@a1.co.uk")] // Atleast 4 chracters before @ sign
        [InlineData("abcd@a.co.uk")] // Atleast 2 chracters after @ sign
        [InlineData("abcd@as.co")]   // should end in either ‘.com’ or ‘.co.uk’.
        [InlineData("abcdasco.uk")] // Missing @ sign
        public void TestCustomerObjectIsValidIfEmailIsEmptyOrIncorrectlyFormattedButAgeIs18OrMore(string email)
        {
            this._customer.Email = email;
            this._customer.DateOfBirth = default;
            var results = _customerValidator.Validate(this._customer);
            Assert.False(results.IsValid);
        }
    }

}
