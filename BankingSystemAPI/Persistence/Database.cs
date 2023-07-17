using BankingSystemAPI.Domain;

namespace BankingSystemAPI.Persistence
{
    public static class Database
    {
        //Almost as good as a real database ;)
        public static List<Customer> CustomerDb { get; set; } = new List<Customer>();
        public static List<Account> AccountDb { get; set; } = new List<Account>();
    }
}
