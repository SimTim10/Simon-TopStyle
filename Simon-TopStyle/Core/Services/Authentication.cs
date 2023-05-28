using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Simon_TopStyle.Core.Interfaces;
using Simon_TopStyle.Models.DTOs;
using Simon_TopStyle.Models.Users;
using System.Security.Claims;

namespace Simon_TopStyle.Core.Authentications
{
    public class Authentication : IAuthentication
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ITokenGenerator _tokenGenerator;

        public Authentication(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager, ITokenGenerator tokenGenerator)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _tokenGenerator = tokenGenerator;
        }
        
        public async Task<string> Register(UserDTO user, string roleName)
        {
            ApplicationUser newUser = new ApplicationUser()
            {
                UserName = user.Name,
                Email = user.Email
            };
            //IEnumerable<Claim> claims;

            var result = await _userManager.CreateAsync(newUser,user.Password);
            if (result.Succeeded)
            {
                var roleExist = await _roleManager.FindByNameAsync(roleName);
                if (roleExist is null)
                {
                    var roleResult = await _roleManager.CreateAsync(new IdentityRole(roleName));
                    if (roleResult.Succeeded)
                    {
                        return ($"{roleName} Good To Go!");
                    }
                    else
                    {
                        return ($"Couldn't Add {roleName} to roles!");
                    }
                }
                
                //var claim = new Claim(type, value);
                //await _userManager.AddClaimAsync(newUser,claim);
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
                //var userClaims = await _userManager.GetClaimsAsync(user);

                return _tokenGenerator.JwtGenerator(user);
            }
            catch (Exception ex)
            {
                return(ex.Message);
            }
            
            

        }
    }
}
