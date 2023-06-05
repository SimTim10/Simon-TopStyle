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
    [Authorize(Roles = "Admin")]
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

        
        [HttpPost("AddProduct")]
        public async Task<IActionResult> AddProduct(ProductDTO product)
        {
            try
            {
                await _adminService.AddNewProduct(product);
                return Ok("New product Successfully has been added.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut]
        [Route("EditProduct")]
        public async Task<IActionResult> EditProduct(int productId, ProductDTO productDTO)
        {
            try
            {
                await _adminService.EditProduct(productId,productDTO);
                return Ok("Successfully Updated Product!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete]
        [Route("DeleteProduct")]
        public async Task<IActionResult> DelProduct(int productId)
        {
            try
            {
                await _adminService.DelProduct(productId);
                return Ok("Deleted");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("EditOrder")]
        public async Task<IActionResult> EditOrder(int productId,int orderId, bool delete)
        {

            await _adminService.EditOrder(productId,orderId,delete);
            return Ok("Done");
        }
    }
}
