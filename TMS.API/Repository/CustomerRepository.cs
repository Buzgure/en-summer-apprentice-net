using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using TMS.API.Model;

namespace TMS.API.Repository
{
    public class CustomerRepository : ICustomerRepository
    {

        private readonly TicketManagementSystemContext _dbContext;

        public CustomerRepository()
        {
            _dbContext = new TicketManagementSystemContext();
        }
        public Customer AddCustomer(Customer customer)
        {
            var entityEntry = _dbContext.Add(customer);
            _dbContext.SaveChanges();
            var c = entityEntry.Entity;
            return c;
        }

        public void DeleteCustomer(Customer customer)
        {
            _dbContext.Remove(customer);
            _dbContext.SaveChanges();
        }

        public IEnumerable<Customer> GetAll()
        {
            return _dbContext.Customers.ToList();
        }

        public async Task<Customer> GetCustomerById(long id)
        {
            var customer = await _dbContext.Customers.Where(c => c.CustomerId == id).FirstOrDefaultAsync();
            if (customer == null)
            {
                throw new Exception("The customer was not found");
            }
            return customer;
        }

        public void UpdateCustomer(Customer customer)
        {
            _dbContext.Entry(customer).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }
    }
}
