namespace CustomerRegistration.API.Validators
{
    public class ValidatorConfig
    {
        /// <summary>
        /// Gets or sets minimun age config setting
        /// </summary>
        public int MinimumCustomerAge { get; set; }

        /// <summary>
        /// Gets or sets mimimum legth for first and last name
        /// </summary>
        public int MinimumNameLength { get; set; }

        /// <summary>
        /// Gets or sets maximum legth for first and last name
        /// </summary>
        public int MaximumNameLength { get; set; }

        /// <summary>
        /// Gets or sets Reg ex pattern for Policy Reference number
        /// </summary>
        public string PolicyReferencePattern { get; set; }

        /// <summary>
        /// Gets or sets Reg ex pattern for Email
        /// </summary>
        public string EmailPattern { get; set; }
    }
}
