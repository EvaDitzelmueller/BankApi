using BankingSystemAPI.Persistence.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;

namespace BankingSystemAPI.Persistence
{
    public class CustomerRepository: ICustomerRepository, IDisposable
    {
        private BankingApiContext context;

        public CustomerRepository(BankingApiContext context)
        {
            this.context = context;
        }

        public IEnumerable<Customer> GetCustomers()
        {
            return context.Customers.ToList();
        }

        public Customer GetCustomerByID(Guid id)
        {
            return context.Customers.Find(id);
        }

        public void InsertCustomer(Customer customer)
        {
            context.Customers.Add(customer);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void DeleteCustomer(Guid id)
        {
            Customer customer = context.Customers.Find(id);
            context.Customers.Remove(customer);
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
