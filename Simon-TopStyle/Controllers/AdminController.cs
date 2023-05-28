using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Simon_TopStyle.Core.Authentications;
using Simon_TopStyle.Core.Interfaces;
using Simon_TopStyle.Models.DTOs;

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


        [HttpGet("GetAllRoles")]
        public IActionResult GetAllRoles()
        {
            var getRoles = _adminService.GetAllRoles();
            return Ok(getRoles);
        }
        [HttpGet]
        [Route("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _adminService.GetAllUsers();
            return Ok(users);
        }



        [Authorize]
        [HttpPost("AddProduct")]

        public IActionResult AddProduct(AddProduct product)
        {
            _adminService.AddNewProduct(product);
            return Ok("Done");
        }
    }
}
