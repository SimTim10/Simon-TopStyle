using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Simon_TopStyle.Core.Interfaces;


namespace Simon_TopStyle.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SetupRolesController : ControllerBase
    {
        private readonly IRolesService _rolesService;

        public SetupRolesController(IRolesService rolesService)
        {
            _rolesService = rolesService;
        }

        [HttpGet("GetAllRoles")]
        public IActionResult GetAllRoles()
        {
            var getRoles = _rolesService.GetAllRoles();
            return Ok(getRoles);
        }
        [HttpGet]
        [Route("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _rolesService.GetAllUsers();
            return Ok(users);
        }

        [HttpPost]
        [Route("CreateRole")]

        public async Task<IActionResult> CreateRole(string newRole)
        {
           var addRole = await _rolesService.CreateRole(newRole);
            return Ok("The role successfully added!");
        }
    }
}
