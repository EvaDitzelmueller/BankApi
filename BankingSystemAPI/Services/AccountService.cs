using BankingSystemAPI.Domain;
using BankingSystemAPI.Persistence;
using System.Reflection.Metadata.Ecma335;

namespace BankingSystemAPI.Services
{
    public class AccountService
    {
        //private readonly Dictionary<string, List<Account>> accounts;

        //public AccountService()
        //{
        //    accounts = new Dictionary<string, List<Account>>();
        //}
        public Account CreateAccount(Customer customer)
        {
            var account = new Account { CustomerId = customer.Id, AccountNumber = Guid.NewGuid(), Balance = 100 };
            Database.AccountDb.Add(account);
            return account;

        }

        public List<Guid> GetAccountNumberByCustomerId(int id)
        {
            var accounts = new List<Guid>();
            foreach (var account in Database.AccountDb)
            {
                if (account.CustomerId == id)
                {
                    accounts.Add(account.AccountNumber);
                }
            }
            return accounts;
        }

        public List<Account> GetAccountsByCustomerId(int customerId)
        {
            var accounts = new List<Account>();
            foreach (var account in Database.AccountDb)
            {
                if (account.CustomerId == customerId)
                {
                    accounts.Add(account);
                }
            }
            return accounts;

            //dont need this anymore????
            //if (Database.AccountDb.TryGetValue(username, out List<Account> userAccounts))
            //{
            //    return userAccounts;
            //}
            //else
            //{
            //    return new List<Account>(); // Return an empty list if the user has no accounts
            //}
        }

        public void DeleteAccount(Guid accountNumber)
        {
            foreach(var account in Database.AccountDb)
            {
                if (account.AccountNumber == accountNumber)
                {
                    Database.AccountDb.Remove(account);
                    break;
                }
            }
        }

        public bool DoesAccountExist(Guid accountNumber)
        {
            foreach (var account in Database.AccountDb.ToList())
            {
                if (account.AccountNumber == accountNumber)
                {
                    return true;
                }
            }
            return false;
        }



        public bool DepositToAccount(Guid accountNumber, decimal amount)
        {
            foreach (var account in Database.AccountDb)
            {
                if (account.AccountNumber == accountNumber)
                {
                    decimal maximumDepositAmount = 10000; // Maximum deposit amount allowed

                    if (amount <= maximumDepositAmount)
                    {
                        account.Balance += amount;
                        return true;
                    }
                    else
                    {
                        throw new InvalidOperationException("Exceeded maximum deposit amount");
                    }
                }
            }
            return false;


            //if(Database.AccountDb.Find(accountNumber)
            //{

            //}
            //if (accounts.ContainsKey(username))
            //{
            //    List<Account> userAccounts = accounts[username];
            //    decimal maximumDepositAmount = 10000; // Maximum deposit amount allowed

            //    if (amount <= maximumDepositAmount)
            //    {
            //        Account account = userAccounts[0]; // Assuming the first account in the list
            //        account.Balance += amount;
            //    }
            //    else
            //    {
            //        throw new InvalidOperationException("Exceeded maximum deposit amount");
            //    }
            //}
        }

        public bool WithdrawFromAccount(Guid accountNumber, decimal amount)
        {
            foreach (var account in Database.AccountDb)
            {
                if (account.AccountNumber == accountNumber)
                {
                    decimal maximumWithdrawalAmount = account.Balance * 0.9m; // 90% of total balance

                    if (account.Balance - amount >= 100 && amount <= maximumWithdrawalAmount)
                    {
                        account.Balance -= amount;
                        return true;
                    }
                    else
                    {
                        throw new InvalidOperationException("Invalid withdrawal amount");
                    }
                }
            }
            return false;



            //        if (accounts.ContainsKey(username))
            //{
            //    List<Account> userAccounts = accounts[username];
            //    decimal maximumWithdrawalAmount = userAccounts[0].Balance * 0.9m; // 90% of total balance

            //    if (userAccounts.Count > 0 && userAccounts[0].Balance - amount >= 100 && amount <= maximumWithdrawalAmount)
            //    {
            //        Account account = userAccounts[0]; // Assuming the first account in the list
            //        account.Balance -= amount;
            //    }
            //    else
            //    {
            //        throw new InvalidOperationException("Invalid withdrawal amount");
            //    }
            //}
        }



        public decimal GetAccountBalance(Guid accountNumber)
        {
            foreach(var account in Database.AccountDb)
            {
                if (account.AccountNumber == accountNumber)
                {
                    return account.Balance;
                }
            }
            return 0;


            //old implementation
            //if (accounts.ContainsKey(username))
            //{
            //    List<Account> userAccounts = accounts[username];
            //    if (userAccounts.Count > 0)
            //    {
            //        Account account = userAccounts[0]; // Assuming the first account in the list
            //        return account.Balance;
            //    }
            //}

            //return 0;
        }

    }
}
