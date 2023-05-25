using Simon_TopStyle.Models.DTOs;

namespace Simon_TopStyle.Core.Interfaces
{
    public interface IAuthentication
    {
        Task Register(UserDTO user);
        Task Login(UserDTO user);
    }
}
