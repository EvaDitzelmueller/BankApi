using BankingSystemAPI.Domain;
using BankingSystemAPI.Persistence;

namespace BankingSystemAPI.Services
{
    public class CustomerService
    {
        private readonly Database _database;

        //similar to account service I would like to isolate the application from the persistance layer
        public CustomerService(Database database)
        {
            _database = database;
        }
        public Customer FindCustomerById(Guid id)
        {
            foreach (var customer in _database.CustomerDb)
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
            return _database.CustomerDb;
        }

        public Customer CreateCustomer(CustomerCreate customer)
        {
            //TODO: verify that customer is actually allowed to create an account (i.e. has good credit score) and generally fulfills the requirements of a KYC process (residency, tax and id documents, aml)
            var c = new Customer ( Guid.NewGuid(), customer.Name );
            _database.CustomerDb.Add(c);
            return c;
        }
    }
}
