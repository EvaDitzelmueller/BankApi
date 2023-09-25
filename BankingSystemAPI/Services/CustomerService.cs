using AutoMapper;
using BankingSystemAPI.Domain;
using BankingSystemAPI.Persistence;
using BankingSystemAPI.Persistence.Models;
using Customer = BankingSystemAPI.Domain.Customer;
using CustomerDTO = BankingSystemAPI.Persistence.Models.Customer;

namespace BankingSystemAPI.Services
{
    public class CustomerService
    {
        //private readonly IDatabase _database;

        private ICustomerRepository customerRepository;
        private readonly AccountService _accountService;

        public CustomerService(AccountService accountService)
        {
            this.customerRepository = new CustomerRepository(new BankingApiContext());
            this._accountService = accountService;
        }

        public CustomerService(AccountService accountService, ICustomerRepository customerRepository)
        {
            this.customerRepository = new CustomerRepository(new BankingApiContext());
            this._accountService = accountService;
        }

        public Customer FindCustomerById(Guid id)
        {
            var customer = customerRepository.GetCustomerByID(id);
            return new Customer(customer.Id,customer.Name);
        }
        public IEnumerable<Customer> FindAll()
        {
            var customersFromDb = customerRepository.GetCustomers();
            //IEnumerable<Customer> customers = _mapper.Map<CustomerDTO[], IEnumerable<Customer>>(customersFromDb);

            // TODO PROPER MAPPING
            var customers = new List<Customer>();

            foreach(var customer in customersFromDb)
            {
                customers.Add(new Customer(customer.Id,customer.Name));
            }
            
            return customers;
        }
        public Customer CreateCustomer(string name)
        {
            var c = new Customer(Guid.NewGuid(), name);
            customerRepository.InsertCustomer(new CustomerDTO(c.Id,c.Name, "test@test.at","Teststraße 123"));
            customerRepository.Save();
            return c;
        }

        public List<Account> GetAccountsByCustomerId(Guid customerId)
        {
            return _accountService.GetAccountsByCustomerId(customerId);
        }
        public bool DeleteCustomer(Guid id)
        {
            customerRepository.DeleteCustomer(id); 
            customerRepository.Save();
            return true;
        }

        //similar to account service I would like to isolate the application from the persistance layer
        //public CustomerService(IDatabase database)
        //{
        //    _database = database;
        //}
        //public Customer FindCustomerById(Guid id)
        //{
        //    foreach (var customer in _database.CustomerDb)
        //    {
        //        if (customer.Id == id)
        //        {
        //            return customer;
        //        }
        //    }
        //    return null;
        //    // TODO: add errorrhandling
        //}

        //public Customer FindCustomerById(Guid id)
        //{

        //}

        //public IEnumerable<Customer> FindAll()
        //{
        //    return _database.CustomerDb;
        //}

        //public Customer CreateCustomer(string name)
        //{
        //    //TODO: verify that customer is actually allowed to create an account (i.e. has good credit score) and generally fulfills the requirements of a KYC process (residency, tax and id documents, aml)
        //    var c = new Customer ( Guid.NewGuid(), name);
        //    _database.CustomerDb.Add(c);
        //    return c;
        //}
        //public bool DeleteCustomer(Guid id)
        //{
        //    foreach (var customer in _database.CustomerDb)
        //    {
        //        if (customer.Id == id)
        //        {
        //            _database.CustomerDb.Remove(customer);
        //            return true;
        //        }
        //    }
        //    return false;
        //}
    }
}
