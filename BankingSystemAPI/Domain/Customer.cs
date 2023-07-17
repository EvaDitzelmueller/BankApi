namespace BankingSystemAPI.Domain
{
    public class Customer
    {

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
        public string Name { get; set; }
    }
}
