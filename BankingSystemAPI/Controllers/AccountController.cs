using BankingSystemAPI.Domain;
using BankingSystemAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace BankingSystemAPI.Controllers;

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
    [HttpPatch]
    public bool Deposit(Guid accountNumber, Transcation transaction)
    {
        return _accountService.DepositToAccount(accountNumber, transaction.Amount);
    }

    [Route("/accounts/{accountNumber}/withdraw")]
    [HttpPatch]
    public bool Withdraw(Guid accountNumber, Transcation transaction)
    {
        return _accountService.WithdrawFromAccount(accountNumber, transaction.Amount);
    }

    ////does the same as withdrawal + deposit in anticipation of transations
    //[Route("/accounts/{accountNumber}")]
    //[HttpPatch]
    //public bool UpdateBalance(Guid accountNumber, Transcation transaction)
    //{
    //    return _accountService.UpdateBalance(accountNumber, transaction.Amount);
    //}
}
