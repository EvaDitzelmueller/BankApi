namespace BankingSystemAPI.Domain
{
    // Due to time constraints this is not fully implemented. It would provide a superior solution to the current implementation.
    // Currently the data model for withdrawing and depositing money is very basic, therefore I would propose to implement a transaction which allows to track money between accounts and better captures the actual workings of a bank. 
    public class Transcation
    {
        public decimal Amount { get; set; }
    }
}
