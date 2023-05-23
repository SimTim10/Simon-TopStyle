using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Simon_TopStyle.Core.Interfaces;
using Simon_TopStyle.Models.DTOs;
using Simon_TopStyle.Models.Users;

namespace Simon_TopStyle.Core.Authentications
{
    public class Authentication : IAuthentication
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public Authentication(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<string> Register(UserDTO user)
        {
            ApplicationUser newUser = new ApplicationUser()
            {
                UserName = user.Name,
                Email = user.Email
            };

            var result = await _userManager.CreateAsync(newUser,user.Password);
            if (result.Succeeded)
            {
                return ("success register");
            }
            else
            {
                return("error register");
            }
        }
        public async Task<string> Login(UserDTO user)
        {
            var result = await _signInManager.PasswordSignInAsync(user.Email,user.Password,false,false);
            if (result.Succeeded)
            {
                return ("success Login");
            }
            else 
            { 
                return("error Login"); 
            }
        }
    }
}
