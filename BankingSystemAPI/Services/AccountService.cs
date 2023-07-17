using BankingSystemAPI.Domain;
using BankingSystemAPI.Persistence;
using System.Reflection.Metadata.Ecma335;

namespace BankingSystemAPI.Services
{
    public class AccountService
    {

        public Account CreateAccount(Guid customerId)
        {
            foreach (var c in Database.CustomerDb)
            {
                if (c.Id == customerId)
                {
                    var account = new Account { CustomerId = customerId, AccountNumber = Guid.NewGuid(), Balance = 100 };
                    Database.AccountDb.Add(account);
                    return account;
                }
                else
                {
                    throw new InvalidOperationException("the customer does not exist");
                }
            }
            return null;
        }

        public List<Guid> GetAccountNumberByCustomerId(Guid customerId)
        {
            var accounts = new List<Guid>();
            foreach (var account in Database.AccountDb)
            {
                if (account.CustomerId == customerId)
                {
                    accounts.Add(account.AccountNumber);
                }
            }
            return accounts;
        }

        public List<Account> GetAccountsByCustomerId(Guid customerId)
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

        public Account FindAccountByNumber(Guid accountNumber)
        {
            foreach (var account in Database.AccountDb)
            {
                if (account.AccountNumber == accountNumber)
                {
                    return account;
                }
            }
            return null;
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
