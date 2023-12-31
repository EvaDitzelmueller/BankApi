﻿namespace BankingSystemAPI.Domain
{
    public class Customer
    {
        //similar to the comment in Account, we should try and separate concerns and add additional fields such as adress, documentation and credit score.

        public Guid Id { get; set; }
        public string Name { get; set; }

        public Customer(Guid Id, string Name)
        {
            this.Id = Id;
            this.Name = Name;
        }
    }

    public class CustomerCreate
    {
        public CustomerCreate()
        {
        }
        public CustomerCreate(string name)
        {
            Name = name;
        }
        public string Name { get; set; }
    }
}
