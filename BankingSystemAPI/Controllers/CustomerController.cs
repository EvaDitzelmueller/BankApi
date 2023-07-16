using BankingSystemAPI.Domain;
using BankingSystemAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace BankingSystemAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ILogger<CustomerController> _logger;
        private readonly CustomerService _customerService;

        public CustomerController(ILogger<CustomerController> logger, CustomerService _customerService)
        {
            _logger = logger;
            this._customerService = _customerService;
        }

        [Route("/users/{id}")]
        [HttpGet]
        public ActionResult<Customer> GetById(int id)
        {
            var customer = _customerService.FindCustomerById(id);

            if(customer == null)
                return NotFound();

            return Ok(customer);
        }

        [Route("/users/")]
        [HttpGet]
        public IEnumerable<Customer> GetAll()
        {
            return _customerService.FindAll();
        }

        [Route("/users/")]
        [HttpPost]
        public Customer Post(Customer customer)
        {
            return _customerService.CreateCustomer(customer);
        }

    }
}