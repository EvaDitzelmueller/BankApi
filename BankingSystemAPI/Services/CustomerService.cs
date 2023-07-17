using BankingSystemAPI.Domain;
using BankingSystemAPI.Persistence;

namespace BankingSystemAPI.Services
{
    public class CustomerService
    {

        public Customer FindCustomerById(Guid id)
        {
            foreach (var customer in Database.CustomerDb)
            {
                if (customer.Id == id)
                {
                    return customer;
                }
            }
            return null;
            // TODO: add errorrhandling
        }

        public IEnumerable<Customer> FindAll()
        {
            return Database.CustomerDb;
        }

        public Customer CreateCustomer(CustomerCreate customer)
        {
            var c = new Customer ( Guid.NewGuid(), customer.Name );
            Database.CustomerDb.Add(c);
            return c;
        }
    }
}
