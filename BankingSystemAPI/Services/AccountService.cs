using BankingSystemAPI.Domain;
using BankingSystemAPI.Persistence;
using System.Reflection.Metadata.Ecma335;

namespace BankingSystemAPI.Services
{
    public class AccountService
    {

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
        }

        public bool DeleteAccount(Guid accountNumber)
        {
            foreach(var account in Database.AccountDb)
            {
                if (account.AccountNumber == accountNumber)
                {
                    Database.AccountDb.Remove(account);
                    return true;
                }
            }
            return false;
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
        }

    }
}
