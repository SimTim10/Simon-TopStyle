using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Simon_TopStyle.Core.Interfaces;

namespace Simon_TopStyle.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        [Route("GetAllProducts")]
        public async Task<IActionResult> GetAllProducts()
        {
            var productList = await _customerService.GetAllProducts();
            return Ok(productList);
        }
    }
}
