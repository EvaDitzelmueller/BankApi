using AutoFixture;
using BankingSystemAPI.Domain;
using BankingSystemAPI.Persistence;
using BankingSystemAPI.Services;
using Castle.Core.Resource;
using FluentAssertions;
using Moq;
using Xunit;

// these tests cover the basic required functionality.
// To do this properly, I would mock the external services (e.g. database,...) and only test the sut.
public class BankingSystemTests
{
    private readonly Database _database = new Database();
    private readonly AccountService _accountService;
    private readonly CustomerService _customerService;
    private const decimal accountCreationBonus = 100;
    private readonly Fixture _fixture;
    private readonly Mock<IDatabase> _databaseMock;

    public BankingSystemTests()
    {
        _fixture = new Fixture();
        _databaseMock = new Mock<IDatabase>();
        //_customerService = new CustomerService(_database);
        _accountService = new AccountService(_databaseMock.Object);
    }

    [Fact]
    public void CreateAccount_AccountCreated_AccountExists()
    {

        // Arrange
        var customer = _fixture.Create<Customer>();
        //_databaseMock.Setup(x => x.CustomerDb.First(It.IsAny<Func<Customer,bool>>())).Returns(customer);
        _databaseMock.SetupAllProperties();
        _databaseMock.SetupProperty(x => x.CustomerDb, new List<Customer> { customer });
        _databaseMock.SetupProperty(x => x.AccountDb, new List<Account> {});
        //var customer = _customerService.CreateCustomer(new CustomerCreate("John Doe"));

        // Act
        var account = _accountService.CreateAccount(customer.Id);

        // Assert
        //Assert.True(_accountService.DoesAccountExist(account.AccountNumber));
        //_databaseMock.Verify(x => x.AccountDb.Add(It.IsAny<Account>()), Times.Once);
        _databaseMock.Object.AccountDb.Should().HaveCount(1);
        _databaseMock.Object.AccountDb.First().CustomerId.Should().Be(customer.Id);
    }

    [Fact]
    public void CreateAccount_UserCreatesTwoAccounts_TwoAccountsExist()
    {
        // Arrange
        var customer = _fixture.Create<Customer>();
        _databaseMock.SetupAllProperties();
        _databaseMock.SetupProperty(x => x.CustomerDb, new List<Customer> { customer });
        _databaseMock.SetupProperty(x => x.AccountDb, new List<Account> { });

        // Act
        var account = _accountService.CreateAccount(customer.Id);
        var account2 = _accountService.CreateAccount(customer.Id);

        // Assert
        _databaseMock.Object.AccountDb.Should().HaveCount(2);
        _databaseMock.Object.AccountDb.First().CustomerId.Should().Be(customer.Id);
        _databaseMock.Object.AccountDb.First().Balance.Should().Be(accountCreationBonus);

        // TODO:Assert second account

    }


    [Fact]
    //Deletion testing: create multiple accounts and delete one --> make sure the correct one is selected for deletion
    //DeleteAccount_DeleteAccountWithId_OnlyAccountWithIdDeleted
    public void DeleteAccount_AccountDeleted_AccountNoLongerExists()
    {
        // Arrange
        // Setup customer
        var customer = _fixture.Create<Customer>();
        _databaseMock.SetupAllProperties();
        _databaseMock.SetupProperty(x => x.CustomerDb, new List<Customer> { customer });

        //Setup accounts
        _databaseMock.SetupProperty(x => x.AccountDb, new List<Account> { });
        var account = _accountService.CreateAccount(customer.Id);
        var account2 = _accountService.CreateAccount(customer.Id);

        // Act
        _accountService.DeleteAccount(account.AccountNumber);

        // Assert
        _databaseMock.Object.AccountDb.Should().HaveCount(1);
        _databaseMock.Object.AccountDb.First().CustomerId.Should().Be(customer.Id);
        _databaseMock.Object.AccountDb.First().AccountNumber.Should().Be(account2.AccountNumber);
    }

    [Fact]
    public void DepositToAccount_ValidDepositAmount_BalanceUpdated()
    {
        // Arrange
        // Setup customer
        var customer = _fixture.Create<Customer>();
        _databaseMock.SetupAllProperties();
        _databaseMock.SetupProperty(x => x.CustomerDb, new List<Customer> { customer });

        //Setup account
        _databaseMock.SetupProperty(x => x.AccountDb, new List<Account> { });
        var account = _accountService.CreateAccount(customer.Id);
        decimal depositAmount = 200;

        // Act
        _accountService.DepositToAccount(account.AccountNumber, depositAmount);

        // Assert
        decimal expectedBalance = depositAmount + accountCreationBonus;
        _databaseMock.Object.AccountDb.Should().HaveCount(1);
        _databaseMock.Object.AccountDb.First().CustomerId.Should().Be(customer.Id);
        _databaseMock.Object.AccountDb.First().Balance.Should().Be(expectedBalance);
    }


//Should I use variables for expectedBalance or just write the value
    [Fact]
    public void WithdrawFromAccount_ValidWithdrawalAmount_BalanceUpdated()
    {
        // Arrange

        // Setup customer
        var customer = _fixture.Create<Customer>();
        _databaseMock.SetupAllProperties();
        _databaseMock.SetupProperty(x => x.CustomerDb, new List<Customer> { customer });

        //Setup account
        _databaseMock.SetupProperty(x => x.AccountDb, new List<Account> { });
        var account = _accountService.CreateAccount(customer.Id);
        decimal depositAmount = 200;
        decimal withdrawAmount = 100;
        account.Balance = depositAmount + accountCreationBonus;
        
        // Act
        _accountService.WithdrawFromAccount(account.AccountNumber, withdrawAmount);

        // Assert
        decimal expectedBalance = accountCreationBonus + depositAmount - withdrawAmount;
        _databaseMock.Object.AccountDb.Should().HaveCount(1);
        _databaseMock.Object.AccountDb.First().CustomerId.Should().Be(customer.Id);
        _databaseMock.Object.AccountDb.First().Balance.Should().Be(expectedBalance);
    }

    [Fact]
    public void WithdrawFromAccount_WithdrawUnderMinimumBalance_ThrowsInvalidOperationException()
    {
        // Arrange
        //var customer = _customerService.CreateCustomer(new CustomerCreate("Abby Normal"));
        //var account = _accountService.CreateAccount(customer.Id);
        decimal withdrawAmount = 1; // this should not be possible, because minimum amount is 100
        // Setup customer
        var customer = _fixture.Create<Customer>();
        _databaseMock.SetupAllProperties();
        _databaseMock.SetupProperty(x => x.CustomerDb, new List<Customer> { customer });

        //Setup account
        _databaseMock.SetupProperty(x => x.AccountDb, new List<Account> { });
        var account = _accountService.CreateAccount(customer.Id);

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => _accountService.WithdrawFromAccount(account.AccountNumber, withdrawAmount));

        // The exception should be thrown, and the balance should remain unchanged
        decimal expectedBalance = accountCreationBonus;
        _databaseMock.Object.AccountDb.Should().HaveCount(1);
        _databaseMock.Object.AccountDb.First().CustomerId.Should().Be(customer.Id);
        _databaseMock.Object.AccountDb.First().Balance.Should().Be(expectedBalance);
    }


    [Fact]
    public void WithdrawFromAccount_OverWithdrawLimit_ThrowsInvalidOperationException()
    {
        // Arrange
        //var customer = _customerService.CreateCustomer(new CustomerCreate("Simon Sais"));
        //var account = _accountService.CreateAccount(customer.Id);
        decimal depositAmount = 9900;
        decimal withdrawAmount = (accountCreationBonus + depositAmount) * 0.95m; // 95% of total balance
        //_accountService.DepositToAccount(account.AccountNumber, depositAmount);

        // Setup customer
        var customer = _fixture.Create<Customer>();
        _databaseMock.SetupAllProperties();
        _databaseMock.SetupProperty(x => x.CustomerDb, new List<Customer> { customer });

        //Setup account
        _databaseMock.SetupProperty(x => x.AccountDb, new List<Account> { });
        var account = _accountService.CreateAccount(customer.Id);
        account.Balance = depositAmount + accountCreationBonus;

        // Act
        Assert.Throws<InvalidOperationException>(() => _accountService.WithdrawFromAccount(account.AccountNumber, withdrawAmount));

        // Assert
        decimal expectedBalance = accountCreationBonus + depositAmount;
        _databaseMock.Object.AccountDb.Should().HaveCount(1);
        _databaseMock.Object.AccountDb.First().CustomerId.Should().Be(customer.Id);
        _databaseMock.Object.AccountDb.First().Balance.Should().Be(expectedBalance); ;
    }

    [Fact]
    public void DepositToAccount_OverDepositLimit_ThrowsInvalidOperationException()
    {
        // Arrange
        decimal depositAmount = 10001; // $10,001
        // Setup customer
        var customer = _fixture.Create<Customer>();
        _databaseMock.SetupAllProperties();
        _databaseMock.SetupProperty(x => x.CustomerDb, new List<Customer> { customer });

        //Setup account
        _databaseMock.SetupProperty(x => x.AccountDb, new List<Account> { });
        var account = _accountService.CreateAccount(customer.Id);
        account.Balance = depositAmount + accountCreationBonus;
        


        // Act and Assert
        Assert.Throws<InvalidOperationException>(() => _accountService.DepositToAccount(account.AccountNumber, depositAmount));

        // The exception should be thrown, and the balance should remain unchanged
        decimal expectedBalance = accountCreationBonus;
        decimal actualBalance = _accountService.GetAccountBalance(account.AccountNumber);
        Assert.Equal(expectedBalance, actualBalance);
    }

}