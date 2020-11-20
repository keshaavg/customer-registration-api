using System.Collections.Generic;
using System.Threading.Tasks;

namespace CustomerRegistration.API.Repository
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<Customer>> GetAll();

        Task<int> Add(Customer customer);
    }
}
