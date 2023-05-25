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
        public string JwtGenerator(UserDTO user)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("MySecretShopKey!"));
            var signingCredentials = new SigningCredentials(secretKey,SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.Email,user.Email)
                //new Claim(ClaimTypes.Role,user.Role)
            };
            var tokenOptions = new JwtSecurityToken(
                issuer: "https://localhost:7108",
                audience: "https://localhost:7108",
                claims: claims,
                expires: DateTime.Now.AddMinutes(20),
                signingCredentials: signingCredentials);

            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
            return new { Token = tokenString }.ToString();
        }
    }
}
