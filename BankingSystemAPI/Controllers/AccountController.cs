using BankingSystemAPI.Domain;
using BankingSystemAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankingSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ILogger<CustomerController> _logger;
        private readonly AccountService _accountService;

        public AccountController(ILogger<CustomerController> logger, AccountService _accountService)
        {
            _logger = logger;
            this._accountService = _accountService;
        }

        [Route("/accounts/")]
        [HttpPost]
        public Account Post(Customer customer)
        {
            return _accountService.CreateAccount(customer);
        }

        [Route("/customer/{customerId}/accounts")]
        [HttpGet]
        public IEnumerable<Account> GetAll(int customerId)
        {
            return _accountService.GetAccountsByCustomerId(customerId);
        }

        [Route("/accounts/{accountNumber}")]
        [HttpDelete]
        public bool Deposit(Guid accountNumber)
        {
            return _accountService.DeleteAccount(accountNumber);
        }

        [Route("/accounts/{accountNumber}/deposit")]
        [HttpPut]
        public bool Deposit(Guid accountNumber, Transcation transaction)
        {
            return _accountService.DepositToAccount(accountNumber, transaction.Amount);
        }

        [Route("/accounts/{accountNumber}/withdraw")]
        [HttpPut]
        public bool Withdraw(Guid accountNumber, Transcation transaction)
        {
            return _accountService.WithdrawFromAccount(accountNumber, transaction.Amount);
        }


    }
}
