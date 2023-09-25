using BankingSystemAPI.Domain;
using BankingSystemAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace BankingSystemAPI.Controllers;

// TODO: Use same naming as requirements (user vs customer)
[ApiController]
[Route("api/[controller]")]
public class CustomerController : ControllerBase
{
    private readonly ILogger<CustomerController> _logger;
    private readonly CustomerService _customerService;

    public CustomerController(ILogger<CustomerController> logger, CustomerService customerService)
    {
        _logger = logger;
        _customerService = customerService;
    }

    [Route("/customers/")]
    [HttpGet]
    public async Task<IEnumerable<Customer>> GetAll()
    {
        //usually I would have to await the db call but as its just a list I don't need it
        return _customerService.FindAll();
    }

    [Route("/customers/{id}")]
    [HttpGet]
    public ActionResult<Customer> GetById(Guid id)
    {
        var customer = _customerService.FindCustomerById(id);

        if(customer == null)
            return NotFound();

        return Ok(customer);
    }


    [Route("/customers/{customerId}/accounts")]
    [HttpGet]
    public IEnumerable<Account> GetAll(Guid customerId)
    {
        return _customerService.GetAccountsByCustomerId(customerId);
    }

    [Route("/customers/")]
    [HttpPost]
    public Customer CreateCustomer(string name)
    {
        return _customerService.CreateCustomer(name);
    }

    [Route("/customers/{customerId}")]
    [HttpDelete]
    public bool DeleteCustomer(Guid customerId)
    {
        return _customerService.DeleteCustomer(customerId);
    }

}