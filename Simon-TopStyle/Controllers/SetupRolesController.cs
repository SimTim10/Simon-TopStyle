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
            await _rolesService.CreateRole(newRole);
            return Ok("The role successfully added!");
        }
        [HttpPost]
        [Route("AddUserToRole")]
        public async Task<IActionResult> AddUserToRole(string email,string roleName)
        {
            try
            {
                await _rolesService.AddUserToRole(email, roleName);
                return Ok("User successfully obtained a new role");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        [Route("GetUserRoles")]
        public async Task<IActionResult> GetUserRoles(string email)
        {
            try
            {
                var rolesList = await _rolesService.GetUserRole(email);
                return Ok(rolesList);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPut]
        [Route("RemoveUserRole")]
        public async Task<IActionResult> RemoveUserRole(string email,string roleName)
        {
            try
            {
                await _rolesService.RemoveUserRole(email, roleName);
                return Ok("Role removed from user.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
