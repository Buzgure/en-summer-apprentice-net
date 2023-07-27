using TMS.API.Model;

namespace TMS.API.Repository
{
    public interface ICustomerRepository
    {
        IEnumerable<Customer> GetAll();

        Task<Customer> GetCustomerById(long id);

        Customer AddCustomer(Customer customer);

        void UpdateCustomer(Customer customer);

        void DeleteCustomer(Customer customer);   
    }
}
