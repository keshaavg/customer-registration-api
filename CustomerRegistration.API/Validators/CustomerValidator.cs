using FluentValidation;
using Microsoft.Extensions.Options;
using System;

namespace CustomerRegistration.API.Validators
{
    /// <summary>
    /// This validator class defines validation rules for <see cref="CustomerDto"/> seperating validation concerns from 
    /// actual class. This uses Fluent Validation which is a popular .NET library for building strongly-typed validation rules.
    /// https://fluentvalidation.net/
    /// </summary>
    public class CustomerValidator : AbstractValidator<CustomerDto>
    {
        /// <summary>
        /// Constructor utilising options pattern to provide strongly typed access to related 
        /// configuration settings needed for validation
        /// Please refer - https://docs.microsoft.com/en-us/aspnet/core/fundamentals/configuration/options?view=aspnetcore-3.1
        /// </summary>
        /// <param name="options"><see cref="IOptions"/> for <see cref="ValidatorConfig"/></param>
        public CustomerValidator(IOptions<ValidatorConfig> options)
        {
            var config = options.Value ?? throw new ArgumentNullException("config", "Config is not initialised");

            // Defines valdation rule for First name. First name is required field and 
            // Length should be between min and max defined in config
            RuleFor(customer => customer.FirstName)
                .NotEmpty()
                .Length(config.MinimumNameLength, config.MaximumNameLength);

            // Defines validation rule for Last name. Last name is required field and Length should be 
            // between min and max defined in config
            RuleFor(customer => customer.LastName)
                .NotEmpty()
                .Length(config.MinimumNameLength, config.MaximumNameLength);

            // Defines validation rule for PolicyReferenceNumber. PolicyReferenceNumber is required field and 
            // must match pattern defined in config. For ex - `AA-000001`
            RuleFor(customer => customer.PolicyReferenceNumber)
                .NotEmpty()
                .Matches(config.PolicyReferencePattern);

            // Deducting minimum customer age defined in config from todays date to compare with Input date of birth
            // (Ignores Hours, minutes and seconds component), If Input date is less than date to compare 
            // after deduction that means customer age is greater than defined minimum age. 
            var dateToCompare = DateTime.Now.AddYears(-config.MinimumCustomerAge).Date;

            // Defines validation rule for DateOfBirth. DateOfBirth is required field if email is blank or null and 
            // customer age must be greater than defined minimum age in config
            RuleFor(customer => customer.DateOfBirth.Date)
                .NotEmpty().WithMessage("Either Date Of Birth or Email is required")
                .When(customer => string.IsNullOrWhiteSpace(customer.Email))
                .LessThanOrEqualTo(dateToCompare)
                .WithMessage($"Age of customer must be greater than or equal to: {config.MinimumCustomerAge}"); ;


            // Defines validation rule for Email. Email is required field if date of birth is not provided and 
            // email match pattren defined in config
            RuleFor(customer => customer.Email)
                .NotEmpty().WithMessage("Either Date Of Birth or Email is required")
                .When(customer => customer.DateOfBirth == default)
                .Matches(config.EmailPattern);

        }
    }
}