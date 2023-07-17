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

        [Route("/customer/")]
        [HttpPost]
        public Customer Post(CustomerCreate customer)
        {
            return _customerService.CreateCustomer(customer);
        }

        [Route("/customer/{id}")]
        [HttpGet]
        public ActionResult<Customer> GetById(Guid id)
        {
            var customer = _customerService.FindCustomerById(id);

            if(customer == null)
                return NotFound();

            return Ok(customer);
        }

        [Route("/customer/")]
        [HttpGet]
        public IEnumerable<Customer> GetAll()
        {
            return _customerService.FindAll();
        }

    }
}