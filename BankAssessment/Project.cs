//public class BankingSystemAPI
//{
//    public static void Main(string[] args)
//    {
//        // You can include any setup or initialization code here.

//        // Create an instance of the BankingSystemAPI
//        var bankingSystem = new BankingSystemAPI();

//        // Perform sample operations
//        bankingSystem.CreateAccount("JohnDoe");
//        bankingSystem.DepositToAccount("JohnDoe", 500);
//        bankingSystem.WithdrawFromAccount("JohnDoe", 200);

//        // Retrieve and display the account balance
//        decimal balance = bankingSystem.GetAccountBalance("JohnDoe");
//        Console.WriteLine("Account balance: $" + balance);

//        // You can include any cleanup or finalization code here.

//        Console.WriteLine("Press any key to exit.");
//        Console.ReadKey();
//    }

//    private readonly Dictionary<string, List<Account>> accounts;

//    public BankingSystemAPI()
//    {
//        accounts = new Dictionary<string, List<Account>>();
//    }


//    public class Account
//    {
//        public int AccountNumber { get; set; }
//        public decimal Amount { get; set; }
//    }


   