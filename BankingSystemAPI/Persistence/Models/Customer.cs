namespace BankingSystemAPI.Persistence.Models
{ public class Customer
        {
            public Customer()
            {
            }

            public Customer(Guid Id, string Name, string Email, string Address)
            {
                this.Id = Id;
                this.Name = Name;
                this.Email = Email;
                this.Address = Address;
            }

        public Guid Id { get; set; }
            public string Name { get; set; }
            public string Email { get; set; }
            public string Address { get; set; }

        }
    }
