using BankingSystemAPI.Domain;
using BankingSystemAPI.Services;
using Xunit;

public class BankingSystemTests
{
    private AccountService _accountService = new AccountService();
    private CustomerService _customerService = new CustomerService();
    private const decimal accountCreationBonus = 100;

    [Fact]
    public void UserCanCreateAccount()
    {
        // Arrange

        Customer customer = new Customer(123,"John Doe");
        _customerService.CreateCustomer(customer);

        // Act
        var account = _accountService.CreateAccount(customer);

        // Assert
        Assert.True(_accountService.DoesAccountExist(account.AccountNumber));
    }

    [Fact]
    public void SameUserCanOpenMultipleAccounts()
    {
        // Arrange
        Customer customer = new Customer(123, "John Doe");

        // Act
        var account = _accountService.CreateAccount(customer);
        var account2 = _accountService.CreateAccount(customer);

        // Assert
        int expectedNumberOfAccounts = 2;
        List<Account> userAccounts = _accountService.GetAccountsByCustomerId(customer.Id);
        Assert.Equal(expectedNumberOfAccounts, userAccounts.Count);

        // Verify the balances of each account
        foreach (Account userAccount in userAccounts)
        {
            decimal actualBalance = userAccount.Balance;
            Assert.Equal(accountCreationBonus, actualBalance);
        }
    }


    [Fact]
    public void UserCanDeleteAccount()
    {
        // Arrange
        Customer customer = new Customer(123, "John Doe");
        var account = _accountService.CreateAccount(customer);

        // Act
        _accountService.DeleteAccount(account.AccountNumber);

        // Assert
        Assert.False(_accountService.DoesAccountExist(account.AccountNumber));
    }

    [Fact]
    public void UserCanDepositToAccount()
    {
        // Arrange
        Customer customer = new Customer(123, "John Doe");
        var account = _accountService.CreateAccount(customer);
        decimal depositAmount = 200;

        // Act
        _accountService.DepositToAccount(account.AccountNumber, depositAmount);

        // Assert
        decimal expectedBalance = depositAmount + accountCreationBonus;
        decimal actualBalance = _accountService.GetAccountBalance(account.AccountNumber);
        Assert.Equal(expectedBalance, actualBalance);
    }

    [Fact]
    public void UserCanWithdrawFromAccount()
    {
        // Arrange
        Customer customer = new Customer(123, "John Doe");
        var account = _accountService.CreateAccount(customer);
        decimal depositAmount = 200;
        decimal withdrawAmount = 100;
        _accountService.DepositToAccount(account.AccountNumber, depositAmount);

        // Act
        _accountService.WithdrawFromAccount(account.AccountNumber, withdrawAmount);

        // Assert
        decimal expectedBalance = accountCreationBonus + depositAmount - withdrawAmount;
        decimal actualBalance = _accountService.GetAccountBalance(account.AccountNumber);
        Assert.Equal(expectedBalance, actualBalance);
    }

    [Fact]
    public void UserCannotWithdrawBelowMinimumBalance()
    {
        // Arrange
        Customer customer = new Customer(123, "John Doe");
        var account = _accountService.CreateAccount(customer);
        decimal withdrawAmount = 1; // this should not be possible, because minimum amount is 100

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => _accountService.WithdrawFromAccount(account.AccountNumber, withdrawAmount));

        // The exception should be thrown, and the balance should remain unchanged
        decimal expectedBalance = accountCreationBonus;
        decimal actualBalance = _accountService.GetAccountBalance(account.AccountNumber);
        Assert.Equal(expectedBalance, actualBalance);
    }


    [Fact]
    public void UserCannotWithdrawExcessiveAmount()
    {
        // Arrange
        Customer customer = new Customer(123, "John Doe");
        var account = _accountService.CreateAccount(customer);
        decimal depositAmount = 9900;
        decimal withdrawAmount = (accountCreationBonus + depositAmount) * 0.95m; // 95% of total balance
        _accountService.DepositToAccount(account.AccountNumber, depositAmount);

        // Act
        Assert.Throws<InvalidOperationException>(() => _accountService.WithdrawFromAccount(account.AccountNumber, withdrawAmount));

        // Assert
        decimal expectedBalance = accountCreationBonus + depositAmount;
        decimal actualBalance = _accountService.GetAccountBalance(account.AccountNumber);
        Assert.Equal(expectedBalance, actualBalance);
    }

    [Fact]
    public void UserCannotDepositExcessiveAmount()
    {
        // Arrange
        Customer customer = new Customer(123, "John Doe");
        var account = _accountService.CreateAccount(customer);
        decimal depositAmount = 10001; // $10,001


        // Act and Assert
        Assert.Throws<InvalidOperationException>(() => _accountService.DepositToAccount(account.AccountNumber, depositAmount));

        // The exception should be thrown, and the balance should remain unchanged
        decimal expectedBalance = accountCreationBonus;
        decimal actualBalance = _accountService.GetAccountBalance(account.AccountNumber);
        Assert.Equal(expectedBalance, actualBalance);
    }

}
