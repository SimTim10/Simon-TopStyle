using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Simon_TopStyle.Core.Interfaces;
using Simon_TopStyle.Data.DataModels;
using Simon_TopStyle.Models.DTOs;
using Simon_TopStyle.Models.Entities;
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
        private readonly TopStyleDBContext _dbContext;

        public Authentication(UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            ITokenGenerator tokenGenerator,
            TopStyleDBContext dbContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _tokenGenerator = tokenGenerator;
            _dbContext = dbContext;
        }
        
        public async Task<string> Register(UserDTO user)
        {
            var existingUser = await _userManager.FindByEmailAsync(user.Email);
            if (existingUser != null)
            {
                throw new Exception("Email already exists.");
            }

            var newUser = new IdentityUser()
            {

                UserName = user.Name,
                Email = user.Email
            };
                        
            var result = await _userManager.CreateAsync(newUser,user.Password);
            
            if (result.Succeeded)
            {
                var customer = new Customer()
                {
                    CustomerName = user.Name,
                    Email = user.Email
                };
                await _dbContext.Customers.AddAsync(customer);
                await _dbContext.SaveChangesAsync();
                
                await _userManager.AddToRoleAsync(newUser, "Customer");
                
                //var claim = new Claim(type, value);
                //await _userManager.AddClaimAsync(newUser,claim);
                return await _tokenGenerator.JwtGenerator(newUser);
            }
            else
            {
                throw new Exception ("Could not register!");
            }
          
        }
        public async Task<string> Login(UserDTO input)
        {
            
            try
            {
                var user = new IdentityUser()
                {
                    UserName = input.Name,
                    Email = input.Email
                };
                await _signInManager.PasswordSignInAsync(input.Email, input.Password, false, false);
                
                var userRoles = await _userManager.GetRolesAsync(user);
                  
                //await _userManager.GetClaimsAsync(user);                    

                return await _tokenGenerator.JwtGenerator(user);
                
                                
            }
            catch
            {
                return("Couldn't log in, check email and/or password");
            }
            
            

        }
    }
}
