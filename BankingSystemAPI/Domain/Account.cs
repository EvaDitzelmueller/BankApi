namespace BankingSystemAPI.Domain
{
    //This is a bit hacky, we should try to separate api schemas, domain models and persistance models due to security reasons, privacy concerns and cleaner architechture.
    public class Account
    {
        public Guid CustomerId { get; set; }
        public decimal Balance { get; set; }
        public Guid AccountNumber { get; set; }

        //This is only a skeleton of a real bank account, we would obviously need more fields such as references to transactions, human readable account number, type of account (current, savings,...)
        //or potentially build an abstraction around and account to support other types of banking services such as stock depos, etc.
    }
}
