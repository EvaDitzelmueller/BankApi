namespace BankingSystemAPI.Domain
{
    public class Account
    {
        public Guid CustomerId { get; set; }
        public decimal Balance { get; set; }
        public Guid AccountNumber { get; set; }
    }
}
