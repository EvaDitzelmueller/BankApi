using BankingSystemAPI.Domain;

namespace BankingSystemAPI.Persistence
{
    public static class Database
    {
        public static List<Customer> CustomerDb { get; set; } = new List<Customer>();
        public static List<Account> AccountDb { get; set; } = new List<Account>();
    }
}
