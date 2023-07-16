namespace BankingSystemAPI.Domain
{
    public class Customer
    {

        public int Id { get; set; }
        public string Name { get; set; }

        public Customer(int Id, string Name)
        {
            this.Id = Id;
            this.Name = Name;
        }
    }
}
