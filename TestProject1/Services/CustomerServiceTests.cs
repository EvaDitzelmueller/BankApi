using AutoFixture;
using BankingSystemAPI.Persistence;
using BankingSystemAPI.Services;
using Moq;
using AutoFixture;
using BankingSystemAPI.Domain;
using BankingSystemAPI.Persistence;
using BankingSystemAPI.Services;
using Castle.Core.Resource;
using FluentAssertions;
using Moq;
using Xunit;

namespace BankingSystemAPITests.cs.Services
{
    public class CustomerServiceTests
    {
        private readonly Database _database = new Database();
        private readonly Mock<AccountService> _accountService;
        private readonly CustomerService _customerService;
        private const decimal accountCreationBonus = 100;
        private readonly Fixture _fixture;
        private readonly Mock<IDatabase> _databaseMock;
        private readonly Mock<ICustomerRepository> _customerRepositoryMock;

        public CustomerServiceTests()
        {
            _fixture = new Fixture();
            _databaseMock = new Mock<IDatabase>();
            _customerRepositoryMock = new Mock<ICustomerRepository>();
            _accountService = new Mock<AccountService>(_databaseMock.Object);
            _customerService = new CustomerService(_accountService.Object, _customerRepositoryMock.Object);
        }

        [Fact]
        public void CreateCustomer_CustomerCreated_CustomerExists()
        {

            // Arrange
            string customerName = _fixture.Create<String>();
            BankingSystemAPI.Persistence.Models.Customer c = new(new Guid(), customerName, "Test", "Test");
            _customerRepositoryMock.Setup(x => x.InsertCustomer(c));
            //_databaseMock.SetupAllProperties();
            //_databaseMock.SetupProperty(x => x.CustomerDb, new List<Customer> { customer });

            // Act
            var customer = _customerService.CreateCustomer(customerName);

            // Assert
            //Assert.True(_accountService.DoesAccountExist(account.AccountNumber));
            //_databaseMock.Verify(x => x.AccountDb.Add(It.IsAny<Account>()), Times.Once);
            customer.Name.Should().Be(customerName);
            customer.Id.Should().NotBeEmpty();
        }

        //Test invalid customer ID?
        //Test that multiple accounts are returned
        //[Fact]
        //public void GetAccountsByCustomerId_ValidCustomerId_AccountReturned()
        //{

        //    // Arrange
        //    // Setup customer
        //    var customer = _fixture.Create<Customer>();
        //    _databaseMock.SetupAllProperties();
        //    _databaseMock.SetupProperty(x => x.CustomerDb, new List<Customer> { customer });

        //    //Setup accounts
        //    _databaseMock.SetupProperty(x => x.AccountDb, new List<Account> { });
        //    var account = _accountService.CreateAccount(customer.Id);

        //    // Act
        //    var a = _customerService.GetAccountsByCustomerId(customer.Id);

        //    // Assert
        //    //Assert.True(_accountService.DoesAccountExist(account.AccountNumber));
        //    //_databaseMock.Verify(x => x.AccountDb.Add(It.IsAny<Account>()), Times.Once);
        //    _databaseMock.Object.AccountDb.Should().HaveCount(1);
        //    Assert.Equal(account.CustomerId, a.First().CustomerId);
        //    _databaseMock.Object.AccountDb.First().CustomerId.Should().Be(a.First().CustomerId);
        //}
    }
}
