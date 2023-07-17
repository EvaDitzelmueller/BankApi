using BankingSystemAPI.Domain;
using BankingSystemAPI.Persistence;
using BankingSystemAPI.Services;
using Xunit;

// these tests cover the basic required functionality.
// However, due to the limitation of the database (static in memory data structures), these tests share some state, which is obviously not good and therefore only work if executed sequentally.
// To do this properly, I would mock the external services (e.g. database,...) and only test the sut.
public class BankingSystemTests
{
    private readonly Database _database = new Database();
    private readonly AccountService _accountService;
    private readonly CustomerService _customerService;
    private const decimal accountCreationBonus = 100;

    public BankingSystemTests()
    {
        _accountService = new AccountService(_database);
        _customerService = new CustomerService(_database);
    }

    [Fact]
    public void UserCanCreateAccount()
    {
        // Arrange

        var customer = _customerService.CreateCustomer(new CustomerCreate("John Doe"));

        // Act
        var account = _accountService.CreateAccount(customer.Id);

        // Assert
        Assert.True(_accountService.DoesAccountExist(account.AccountNumber));
    }

    [Fact]
    public void SameUserCanOpenMultipleAccounts()
    {
        // Arrange
        var customer = _customerService.CreateCustomer(new CustomerCreate("Aida Bugg"));

        // Act
        var account = _accountService.CreateAccount(customer.Id);
        var account2 = _accountService.CreateAccount(customer.Id);

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
        var customer = _customerService.CreateCustomer(new CustomerCreate("Hugo First"));
        var account = _accountService.CreateAccount(customer.Id);

        // Act
        _accountService.DeleteAccount(account.AccountNumber);

        // Assert
        Assert.False(_accountService.DoesAccountExist(account.AccountNumber));
    }

    [Fact]
    public void UserCanDepositToAccount()
    {
        // Arrange
        var customer = _customerService.CreateCustomer(new CustomerCreate("Max Mustermann"));
        var account = _accountService.CreateAccount(customer.Id);
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
        var customer = _customerService.CreateCustomer(new CustomerCreate("Polly Pipe"));
        var account = _accountService.CreateAccount(customer.Id);
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
        var customer = _customerService.CreateCustomer(new CustomerCreate("Abby Normal"));
        var account = _accountService.CreateAccount(customer.Id);
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
        var customer = _customerService.CreateCustomer(new CustomerCreate("Simon Sais"));
        var account = _accountService.CreateAccount(customer.Id);
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
        var customer = _customerService.CreateCustomer(new CustomerCreate("Theresa Green"));
        var account = _accountService.CreateAccount(customer.Id);
        decimal depositAmount = 10001; // $10,001


        // Act and Assert
        Assert.Throws<InvalidOperationException>(() => _accountService.DepositToAccount(account.AccountNumber, depositAmount));

        // The exception should be thrown, and the balance should remain unchanged
        decimal expectedBalance = accountCreationBonus;
        decimal actualBalance = _accountService.GetAccountBalance(account.AccountNumber);
        Assert.Equal(expectedBalance, actualBalance);
    }

}