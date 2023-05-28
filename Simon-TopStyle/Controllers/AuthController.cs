using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Simon_TopStyle.Core.Interfaces;
using Simon_TopStyle.Models.DTOs;

namespace Simon_TopStyle.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthentication _authentication;

        
        public AuthController(IAuthentication authentication)
        {
            _authentication = authentication;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(UserDTO user, string roleName)
        {
            try
            {
                var result = await _authentication.Register(user, roleName);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login(UserDTO user)
        {
            try
            {
                var result = await _authentication.Login(user);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }
    }
}
