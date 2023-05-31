using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Simon_TopStyle.Core.Authentications;
using Simon_TopStyle.Core.Interfaces;
using Simon_TopStyle.Models.DTOs;
using Simon_TopStyle.Models.Entities;

namespace Simon_TopStyle.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;
        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }
        [HttpGet("GetAllProducts")]
        public IActionResult GetProducts()
        {
            var allproducts = _adminService.GetProducts();
            return Ok(allproducts);
            
        }

        [Authorize]
        [HttpPost("AddProduct")]
        public IActionResult AddProduct(ProductDTO product, int categoryId)
        {
            _adminService.AddNewProduct(product, categoryId);
            return Ok("Done");
        }
    }
}
