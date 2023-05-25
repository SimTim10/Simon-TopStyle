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
        private readonly ITokenGenerator _tokenGenerator;

        public Authentication(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ITokenGenerator tokenGenerator)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenGenerator = tokenGenerator;
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
                return _tokenGenerator.JwtGenerator(user);
            }
            else
            {
                throw new Exception ("Could not register!");
            }
          
        }
        public async Task<string> Login(UserDTO user)
        {
            try
            {
                await _signInManager.PasswordSignInAsync(user.Email, user.Password, false, false);
                return _tokenGenerator.JwtGenerator(user);
            }
            catch (Exception ex)
            {
                return(ex.Message);
            }
            
            

        }
    }
}
