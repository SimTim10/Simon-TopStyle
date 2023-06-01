using Microsoft.AspNetCore.Authorization;
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

        [HttpGet]
        [Route("Search")]
        public async Task<IActionResult> SearchProducts(string searchInput)
        {
            try
            {
                var result = await _customerService.SearchProduct(searchInput);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Authorize(Roles = "Customer")]
        [HttpGet]
        [Route("GetMyInfo")]
        public async Task<IActionResult> GetCustomer(string email)
        {
            try
            {
                var result = await _customerService.GetCustomer(email);
                return Ok(result);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
