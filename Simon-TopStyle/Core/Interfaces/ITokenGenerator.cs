using Microsoft.AspNetCore.Identity;
using Simon_TopStyle.Models.DTOs;

namespace Simon_TopStyle.Core.Interfaces
{
    public interface ITokenGenerator
    {
        Task<string> JwtGenerator(IdentityUser user);
    }
}
