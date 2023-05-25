using Simon_TopStyle.Models.DTOs;

namespace Simon_TopStyle.Core.Interfaces
{
    public interface IAuthentication
    {
        Task<string> Register(UserDTO user);
        Task<string> Login(UserDTO user);
    }
}
