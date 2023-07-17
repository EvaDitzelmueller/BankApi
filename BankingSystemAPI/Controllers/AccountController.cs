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
        public Account Post(Guid customerId)
        {
            return _accountService.CreateAccount(customerId);
        }

        [Route("/customer/{customerId}/accounts")]
        [HttpGet]
        public IEnumerable<Account> GetAll(Guid customerId)
        {
            return _accountService.GetAccountsByCustomerId(customerId);
        }

        [Route("/accounts/{accountNumber}")]
        [HttpGet]
        public ActionResult<Account> GetByAccountNumber(Guid accountNumber)
        {
            var account = _accountService.FindAccountByNumber(accountNumber);

            if (account == null)
                return NotFound();

            return Ok(account);
        }

        [Route("/accounts/{accountNumber}")]
        [HttpDelete]
        public bool DeleteAccount(Guid accountNumber)
        {
            return _accountService.DeleteAccount(accountNumber);
        }

        // This route should be refactored to work with transactions in the future
        // There should not be the need to distinquish between deposit and withdrawal, they could be unified in a transaction data model
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
