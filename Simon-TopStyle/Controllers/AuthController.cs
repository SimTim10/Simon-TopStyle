using Microsoft.AspNetCore.Http;
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
        public async Task<IActionResult> Register(UserDTO user)
        {
            await _authentication.Register(user);
            return Ok();
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login(UserDTO user)
        {
            await _authentication.Login(user);
            return Ok();
        }
    }
}
