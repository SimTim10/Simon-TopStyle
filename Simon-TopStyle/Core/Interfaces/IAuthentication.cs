using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Simon_TopStyle.Models.DTOs;
using Simon_TopStyle.Models.Users;

namespace Simon_TopStyle.Core.Interfaces
{
    public interface IAuthentication
    {
       
        Task<string> Register(UserDTO user, string roleName);
        Task<string> Login(UserDTO user);
    }
}
