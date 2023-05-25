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

        public async Task Register(UserDTO user)
        {
            ApplicationUser newUser = new ApplicationUser()
            {
                UserName = user.Name,
                Email = user.Email
            };

            await _userManager.CreateAsync(newUser,user.Password);
          
        }
        public async Task Login(UserDTO user)
        {
             await _signInManager.PasswordSignInAsync(user.Email,user.Password,false,false);
           
        }
    }
}
