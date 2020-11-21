using AutoMapper;

namespace CustomerRegistration.API
{
    /// <summary>
    /// Defines automapper mapping profiles
    /// </summary>
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            // Defines map from CustomerDto to Customer
            CreateMap<CustomerDto, Customer>(); 
        }
    }
}
