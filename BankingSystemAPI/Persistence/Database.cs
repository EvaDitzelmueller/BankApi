using BankingSystemAPI.Domain;

namespace BankingSystemAPI.Persistence
{
    public class Database : IDatabase
    {
        //Almost as good as a real database ;) but next time I'd propose to use Entity Framework with a relational database
        //This obviously has severe drawbacks, like concurrency issues, code quality and testability, just a quick hack to get started without a database.
        public List<Customer> CustomerDb { get; set; } = new();
        public List<Account> AccountDb { get; set; } = new();
    }

    public interface IDatabase
    {
        public List<Customer> CustomerDb { get; set; } 
        public List<Account> AccountDb { get; set; } 
    }
}
