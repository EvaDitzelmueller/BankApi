using BankingSystemAPI.Persistence.Models;

namespace BankingSystemAPI.Persistence
{
    public interface ICustomerRepository
    {
        IEnumerable<Customer> GetCustomers();
        Customer GetCustomerByID(Guid customerId);
        void InsertCustomer(Customer customer);
        void Save();
        //TODO Add other crud
        void DeleteCustomer(Guid id);
    }
}
