using BankingSystemAPI.Domain;
using BankingSystemAPI.Persistence;

namespace BankingSystemAPI.Services
{
    public class CustomerService
    {

        public Customer FindCustomerById(int id)
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

        public Customer CreateCustomer(Customer customer)
        {
            Database.CustomerDb.Add(customer);
            return customer;
        }
    }
}
