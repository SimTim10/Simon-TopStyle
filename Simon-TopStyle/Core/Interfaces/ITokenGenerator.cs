using Simon_TopStyle.Models.DTOs;

namespace Simon_TopStyle.Core.Interfaces
{
    public interface ITokenGenerator
    {
        public string JwtGenerator(UserDTO user);
    }
}
