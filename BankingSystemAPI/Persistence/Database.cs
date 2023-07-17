using BankingSystemAPI.Domain;

namespace BankingSystemAPI.Persistence
{
    public static class Database
    {
        //Almost as good as a real database ;) but next time I'd propose to use Entity Framework with a relational database
        //This obviously has severe drawbacks, like concurrency issues, code quality and testability, just a quick hack to get started without a database.
        public static List<Customer> CustomerDb { get; set; } = new List<Customer>();
        public static List<Account> AccountDb { get; set; } = new List<Account>();
    }
}
