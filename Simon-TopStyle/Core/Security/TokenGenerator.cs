using IdentityServer4.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Simon_TopStyle.Core.Interfaces;
using Simon_TopStyle.Models.DTOs;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Simon_TopStyle.Core.Security
{
    public class TokenGenerator : ITokenGenerator
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public TokenGenerator(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<string> JwtGenerator(IdentityUser user)
        {
            
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("MySecretShopKey!"));
            var signingCredentials = new SigningCredentials(secretKey,SecurityAlgorithms.HmacSha256);
            var claims = await GetAllValidClaims(user);

            /*var tokenOptions = new JwtSecurityToken(
                
                issuer: "https://localhost:7108",
                audience: "https://localhost:7108",
                claims: claims,
                expires: DateTime.Now.AddMinutes(20),
                signingCredentials: signingCredentials);
            

            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
            return new { Token = tokenString }.ToString();*/

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddMinutes(30),
                SigningCredentials = signingCredentials
            };
            var token = new JwtSecurityTokenHandler().CreateToken(tokenDescriptor);
            var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

            
            return new { Token = jwtToken }.ToString();
        }

        private async Task<List<Claim>> GetAllValidClaims(IdentityUser user)
        {
            var _options = new IdentityOptions();
            var claims = new List<Claim>()
            {
                new Claim("Id", user.Id),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Sub,user.Email),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
                
            };

            //Getting Claims of our user and adding them to this list of claims.

            var userClaims = await _userManager.GetClaimsAsync(user);
            claims.AddRange(userClaims);

            //Getting the user roles and add it to new claims

            var userRoles = await _userManager.GetRolesAsync(user);  

            foreach(var userRole in userRoles)
            {
                var everyRole = await _roleManager.FindByNameAsync(userRole);
                if(everyRole != null)
                {
                    claims.Add(new Claim(ClaimTypes.Role, userRole));

                    var roleClaims = await _roleManager.GetClaimsAsync(everyRole);
                    foreach( var roleClaim in roleClaims)
                    {
                        claims.Add(roleClaim);
                    }
                }

            }
            return claims;
            
        }
    }
}
